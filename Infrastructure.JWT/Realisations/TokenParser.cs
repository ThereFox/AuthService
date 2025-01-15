using Application.Interfaces;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuntificationService.Domain.Entitys;
using Infrastructure.Tokens.JWT.JWTCreater;
using Infrastructure.Tokens.JWT.Payloads;
using Infrastructure.Tokens.JWT.Payloads.SubTypes;

namespace Infrastructure.Tokens.JWT.Driver
{
    public class TokenParser : ITokenParser
    {
        private const string MyServiceName = "ThereFoxAuthService";
        private readonly JWTTokenParser _parser;

        public TokenParser(JWTTokenParser parser)
        {
            _parser = parser;
        }

        public Result<AuthTokenPayload> GetInfoFromAuthToken(AuthorisationToken token)
        {
            var content = _parser.GetPayloadFromToken<AuthPayload>(token.Token);

            if (content.IsFailure)
            {
                return content.ConvertFailure<AuthTokenPayload>();
            }
            
            var payload = content.Value;

            if (payload.Issuer.Equals(MyServiceName) == false)
            {
                return Result.Failure<AuthTokenPayload>("Is not my token (anouther issuer)");
            }

            if (payload.ExpirationDate.stemp <= DateTimeTimeStemp.FromDateTime(DateTime.Now).stemp)
            {
                return Result.Failure<AuthTokenPayload>("Expiration date was achibed");
            }

            var validateUserRole = UserRole.Create(payload.RoleId);

            if (validateUserRole.IsFailure)
            {
                return Result.Failure<AuthTokenPayload>("unsupported user role");
            }
            
            return AuthTokenPayload.Create(payload.UserId, validateUserRole.Value);
        }

        public Result<RefreshTokenPayload> GetInfoFromRefreshToken(RefreshToken token)
        {
            var content = _parser.GetPayloadFromToken<RefreshPayload>(token.Token);

            if (content.IsFailure)
            {
                return Result.Failure<RefreshTokenPayload>($"Error with refresh token: {content.Error}");
            }
            
            var payload = content.Value;

            if (payload.Issuer.Equals(MyServiceName) == false)
            {
                return Result.Failure<RefreshTokenPayload>("Is not my token (anouther issuer)");
            }

            if (payload.ExpirationDate.stemp <= DateTimeTimeStemp.FromDateTime(DateTime.Now).stemp)
            {
                return Result.Failure<RefreshTokenPayload>("Expiration date was achibed");
            }

            if (payload.NotActiveBefore.stemp > DateTimeTimeStemp.FromDateTime(DateTime.Now).stemp)
            {
                return Result.Failure<RefreshTokenPayload>("Is not active");
            }

            return RefreshTokenPayload.Create(payload.OwnerId, payload.IssuetAt.ToDateTime(), payload.ExpirationDate.ToDateTime());

        }
    }
}

using Application.Interfaces;
using AuntificationService.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuntificationService.Domain.ValueObjects;
using Domain.ValueObjects;
using Infrastructure.Tokens.JWT.JWTCreater;
using Infrastructure.Tokens.JWT.Payloads;
using Infrastructure.Tokens.JWT.Payloads.SubTypes;

namespace Infrastructure.Tokens.JWT.Driver
{
    public class TokenGenerator : ITokenGenerator
    {
        private const string MyServiceName = "ThereFoxAuthService";
        
        private readonly JWTTokenGenerator _generator;

        public TokenGenerator(JWTTokenGenerator generator)
        {
            _generator = generator;
        }

        public Domain.Entitys.Tokens Generate(User user)
        {
            var authPayload = getAuthPayload(user);

            var refreshPayload = getRefreshPayload(user);
            
            var authToken = _generator.CreateToken(authPayload);
            var refreshToken = _generator.CreateToken(refreshPayload);

            var commonValidateAuthToken = Token.Create(authToken);
            var commonValidateRefreshToken = Token.Create(refreshToken);

            if (commonValidateAuthToken.IsFailure || commonValidateRefreshToken.IsFailure)
            {
                throw new InvalidCastException();
            }
            
            var validateAuthToken = AuthorisationToken.Create(commonValidateAuthToken.Value);
            var validateRefreshToken = RefreshToken.Create(commonValidateRefreshToken.Value);

            if (validateAuthToken.IsFailure || validateRefreshToken.IsFailure)
            {
                throw new InvalidCastException();
            }
            
            var validatePair = TokensPair.Create(validateAuthToken.Value, validateRefreshToken.Value);

            if (validatePair.IsFailure)
            {
                throw new InvalidCastException();
            }

            var validateTokens = Domain.Entitys.Tokens.Create(DateTime.Now, validatePair.Value, user);

            if (validateTokens.IsFailure)
            {
                throw new InvalidCastException();
            }

            return validateTokens.Value;
        }

        private AuthPayload getAuthPayload(User user)
        {
            var ttl = user.Role == UserRole.Admin ? TimeSpan.FromHours(3) : TimeSpan.FromMinutes(30);

            var authPayload = new AuthPayload()
            {
                Issuer = MyServiceName,
                IssuetAt = DateTimeTimeStemp.FromDateTime(DateTime.Now),
                RoleId = user.Role.RoleId,
                UserId = user.Id,
                ExpirationDate = DateTimeTimeStemp.FromDateTime(DateTime.Now + ttl)
            };
            
            return authPayload;
        }

        private RefreshPayload getRefreshPayload(User user)
        {
            var refreshTtl = user.Role == UserRole.Admin ? TimeSpan.FromDays(3) : TimeSpan.FromDays(1);
            
            var authTtl = user.Role == UserRole.Admin ? TimeSpan.FromHours(3) : TimeSpan.FromMinutes(30);

            var authPayload = new RefreshPayload()
            {
                Issuer = MyServiceName,
                OwnerId = user.Id,
                IssuetAt = DateTimeTimeStemp.FromDateTime(DateTime.Now),
                ExpirationDate = DateTimeTimeStemp.FromDateTime(DateTime.Now + refreshTtl),
                NotActiveBefore = DateTimeTimeStemp.FromDateTime(DateTime.Now + authTtl),
                RefreshTimeout = DateTimeTimeStemp.FromDateTime(DateTime.Now + refreshTtl)
            };
            
            return authPayload;
        }
    }
}

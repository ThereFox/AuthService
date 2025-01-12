using Application.Interfaces;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Tokens.JWT.JWTCreater;
using Infrastructure.Tokens.JWT.Payloads;

namespace Infrastructure.Tokens.JWT.Driver
{
    public class TokenParser : ITokenParser
    {
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
            
            return Result.Failure<AuthTokenPayload>("not realised");
        }

        public Result<object> GetInfoFromRefreshToken(RefreshToken token)
        {
            var content = _parser.GetPayloadFromToken<RefreshPayload>(token.Token);

            if (content.IsFailure)
            {
                return content.ConvertFailure<AuthTokenPayload>();
            }
            
            return Result.Failure<AuthTokenPayload>("not realised");
        }
    }
}

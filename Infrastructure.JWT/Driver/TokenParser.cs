using Application.Interfaces;
using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Driver
{
    public class TokenParser : ITokenParser
    {
        public Result<AuthTokenPayload> GetInfoFromAuthToken(AuthorisationToken token)
        {
            return Result.Failure<AuthTokenPayload>("Not realised");
            throw new NotImplementedException();
        }

        public Result<object> GetInfoFromRefreshToken(RefreshToken token)
        {
            return Result.Failure<object>("Not realised");
            throw new NotImplementedException();
        }
    }
}

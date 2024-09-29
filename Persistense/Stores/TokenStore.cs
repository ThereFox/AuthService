using Application.Stores;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stores
{
    public class TokenStore : ITokenStore
    {
        public async Task<Result> DeactivateTokens(Tokens tokens)
        {
            return Result.Failure("Not realised");
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> IsActiveToken(Token token)
        {
            return Result.Failure<bool>("Not realised");
            throw new NotImplementedException();
        }

        public async Task<Result> SaveTokens(Tokens tokens)
        {
            return Result.Failure("Not realised");
            throw new NotImplementedException();
        }
    }
}

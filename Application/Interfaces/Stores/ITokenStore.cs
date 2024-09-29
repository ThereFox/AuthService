using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stores
{
    public interface ITokenStore
    {
        public Task<Result<bool>> IsActiveToken(Token token);
        public Task<Result> SaveTokens(Tokens tokens);
        public Task<Result> DeactivateTokens(Tokens tokens);

    }
}

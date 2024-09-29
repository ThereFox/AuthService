using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.ValueObjects
{
    public class AuthorisationToken : ValueObject
    {
        public Token Token { get; init; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token.Value;
        }

        private AuthorisationToken(Token token)
        {
            Token = token;
        }

        public static Result<AuthorisationToken> Create(Token token)
        {
            return Result.Success(new AuthorisationToken(token));
        }    
    }
}

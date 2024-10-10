using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class TokensPair : ValueObject
    {
        public AuthorisationToken AuthToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AuthToken.Token;
            yield return RefreshToken.Token;
        }

        private TokensPair(AuthorisationToken authToken, RefreshToken refreshToken)
        {
            AuthToken = authToken;
            RefreshToken = refreshToken;
        }

        public static Result<TokensPair> Create(AuthorisationToken auth, RefreshToken refreshToken)
        {
            return Result.Success(new TokensPair(auth, refreshToken));
        }
    }
}

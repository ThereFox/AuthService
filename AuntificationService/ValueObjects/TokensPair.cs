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
        public AuthorisationToken Auth { get; set; }
        public RefreshToken RefreshToken { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Auth.Token;
            yield return RefreshToken.Token;
        }
    }
}

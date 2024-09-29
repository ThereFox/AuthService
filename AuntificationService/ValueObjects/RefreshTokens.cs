using CSharpFunctionalExtensions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntificationService.Domain.ValueObjects
{
    public class RefreshToken : ValueObject
    {
        public Token Token {  get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        private RefreshToken(Token token)
        {
            Token = token;
        }

        public static Result<RefreshToken> Create(Token token)
        {
            return Result.Success(new RefreshToken(token));
        }

    }
}

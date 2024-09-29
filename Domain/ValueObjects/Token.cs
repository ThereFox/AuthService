using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Token : ValueObject
    {
        public string Value { get; init; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        protected Token(string value)
        {
            Value = value;
        }

        public static Result<Token> Create(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<Token>("empty input");
            }

            return Result.Success(new Token(value));
        }

    }
}

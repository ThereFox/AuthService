using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Token : ValueObject
    {
        private const string TokenFormatRegExp = "[A-Za-z0-9]{15,}.[A-Za-z0-9]{15,}.[A-Za-z0-9]{15,}"; 

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

            if (Regex.IsMatch(value, TokenFormatRegExp) == false)
            {
                return Result.Failure<Token>("invalid token format");
            }
            
            return Result.Success(new Token(value));
        }

    }
}

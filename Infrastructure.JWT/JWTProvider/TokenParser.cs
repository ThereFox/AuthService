using Common;
using CSharpFunctionalExtensions;
using Infrastructure.Tokens.JWT.Encodings;
using Infrastructure.Tokens.JWT.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.JWTCreater
{
    internal class TokenParser
    {
        private readonly IEncodingReader _encoder;

        internal Result<T> Parse<T>(string token) where T: PayloadBase
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<T>("empty input");
            }

            var parts = token.Split('.');

            if(parts.Length != 3)
            {
                return Result.Failure<T>("Invalid token format");
            }

            if (haveContentChanges(token))
            {
                return Result.Failure<T>("Token info was modificated");
            }

            return readEncodedPayload<T>(parts[1]);

        }


        private bool haveContentChanges(string content)
        {
            return false;
        }

        private Result<T> readEncodedPayload<T>(string input) where T: PayloadBase
        {
            var decodedPayload = _encoder.Decode(input);

            return JsonResultConverter.Deserialise<T>(decodedPayload);
        }

    }
}

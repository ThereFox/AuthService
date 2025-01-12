using Common;
using CSharpFunctionalExtensions;
using Infrastructure.Tokens.JWT.Encodings;
using Infrastructure.Tokens.JWT.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Infrastructure.Tokens.JWT.JWTCreater
{
    public class JWTTokenParser
    {
        private readonly JWTTokenSigner _signer;
        private readonly IEncodingReader _encoder;

        public JWTTokenParser(JWTTokenSigner signer, IEncodingReader encoder)
        {
            _signer = signer;
            _encoder = encoder;
        }

        public Result<PayloadType> GetPayloadFromToken<PayloadType>(Token token)
            where PayloadType : PayloadBase, new()
        {
            if (token == null)
            {
                return Result.Failure<PayloadType>("Token is null");
            }
            
            var parts = token.Value.Split('.');
            
            var header = parts[0];
            var body = parts[1];
            var signature = parts[2];

            if (IsInitialToken(header, body, signature) == false)
            {
                return Result.Failure<PayloadType>("token was modified");
            }

            return getPayloadFromBody<PayloadType>(body);
        }

        private bool IsInitialToken(string header, string body, string signature)
        {
            try
            {
                var resignedString = _signer.GetSignature(header, body);

                return resignedString.Equals(signature);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Result<PayloadType> getPayloadFromBody<PayloadType>(string body)
            where PayloadType : PayloadBase, new()
        {
            var payloadJson = _encoder.Decode(body);
            return JsonResultConverter.Deserialise<PayloadType>(payloadJson);
        }
        
    }
}

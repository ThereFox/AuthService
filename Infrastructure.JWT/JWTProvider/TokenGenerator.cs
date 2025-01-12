using Infrastructure.Tokens.JWT.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using CSharpFunctionalExtensions;
using Infrastructure.Tokens.JWT.Encodings;
using Newtonsoft.Json;

namespace Infrastructure.Tokens.JWT.JWTCreater
{
    public class JWTTokenGenerator
    {
        private const string Header = "{'alg': 'HS256','typ': 'JWT'}";
        private readonly IEncodingReader _encoder;
        private readonly JWTTokenSigner _signer;

        public JWTTokenGenerator(IEncodingReader encoder, JWTTokenSigner signer)
        {
            _encoder = encoder;
            _signer = signer;
        }

        public string CreateToken<T>(T payload) where T : PayloadBase
        {
            var payloadJson = JsonConvert.SerializeObject(payload);
            
            var header = _encoder.Encode(Header);
            var content = _encoder.Encode(payloadJson);
            var sign = _signer.GetSignature(header, content);

            return $"{header}.{content}.{sign}";
        }
    }
}

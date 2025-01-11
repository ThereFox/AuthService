using Infrastructure.Tokens.JWT.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.JWTCreater
{
    internal class TokenGenerator
    {
        internal string CreateToken<T>(T payload, TimeSpan TTL) where T : PayloadBase
        {
            return string.Empty;
        }
    }
}

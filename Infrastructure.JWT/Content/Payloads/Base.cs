using Infrastructure.Tokens.JWT.Payloads.SubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Payloads
{
    internal abstract class PayloadBase
    {
        [JsonPropertyName("iss")]
        public string Issuer { get; set; }
        [JsonPropertyName("exp")]
        public DateTimeTimeStemp ExpirationDate { get; set; }
        [JsonPropertyName("iat")]
        public DateTimeTimeStemp IssuetAt {  get; set; }
    }
}

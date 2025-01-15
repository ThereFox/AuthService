using Infrastructure.Tokens.JWT.Payloads.SubTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Payloads
{
    internal class RefreshPayload : PayloadBase
    {
        [JsonPropertyName("own")]
        public Guid OwnerId { get; set; }
        
        [JsonPropertyName("nbf")]
        public DateTimeTimeStemp NotActiveBefore { get; set; }
        
        [JsonPropertyName("ref")]
        public DateTimeTimeStemp RefreshTimeout { get; set; }
    }
}

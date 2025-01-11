using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Payloads
{
    internal class AuthPayload : PayloadBase
    {
        [JsonPropertyName("id")]
        public Guid UserId { get; set; }
        [JsonPropertyName("rl")]
        public int RoleId { get; set; }
    }
}

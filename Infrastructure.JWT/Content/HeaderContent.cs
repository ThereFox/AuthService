using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Content
{
    internal class HeaderContent
    {
        [JsonPropertyName("alg")]
        public string Algorithm { get; set; }

        [JsonPropertyName("typ")]
        public string Type { get; set; }

        [JsonConstructor]
        public HeaderContent(string alg, string typ)
        {
            Algorithm = alg;
            Type = typ;
        }
    }
}

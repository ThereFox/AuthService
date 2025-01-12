using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Encodings.Realisation
{
    public class Base64Encoder : IEncodingReader
    {
        public string Decode(string input)
        {
            var encodedInputeBytes = Convert.FromBase64String(input);
            return Encoding.ASCII.GetString(encodedInputeBytes);
        }

        public string Encode(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }
    }
}

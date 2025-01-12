using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT.Encodings
{
    public interface IEncodingReader
    {
        public string Encode(string input);
        public string Decode(string input);
    }
}

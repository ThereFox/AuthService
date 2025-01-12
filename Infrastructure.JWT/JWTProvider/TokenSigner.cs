using System.Security.Cryptography;
using System.Text;
using Infrastructure.Tokens.JWT.Encodings;

namespace Infrastructure.Tokens.JWT.JWTCreater;

public class JWTTokenSigner
{
    private readonly HMACSHA256 _encoder;
    private readonly string _secret;

    public JWTTokenSigner(HMACSHA256 encoder, string secret)
    {
        _encoder = encoder;
        _secret = secret;
    }

    public string GetSignature(string header, string body)
    {
        var bytes = Encoding.UTF8.GetBytes($"{_secret}.{header}.{body}");
        var encodedBytes = _encoder.ComputeHash(bytes);
        return Convert.ToBase64String(encodedBytes);
    }
}
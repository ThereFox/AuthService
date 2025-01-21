using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Domain.ValueObjects;
using Infrastructure.Tokens.JWT.Payloads;
using Infrastructure.Tokens.JWT.Encodings.Realisation;
using Infrastructure.Tokens.JWT.JWTCreater;
using Infrastructure.Tokens.JWT.Payloads.SubTypes;

namespace UnitTests;

public class TokenTests
{
    private readonly JWTTokenGenerator _generator;
    private readonly JWTTokenParser _parser;

    public TokenTests()
    {
        var key = Encoding.UTF8.GetBytes("key");
        _generator = new JWTTokenGenerator(
            new Base64Encoder(),
            new JWTTokenSigner(
                new HMACSHA256(key),
                "secret"
            )
        );
        _parser = new JWTTokenParser(
            new JWTTokenSigner(
                new HMACSHA256(key),
                "secret"
            ),
            new Base64Encoder()
        );
    }

    [Fact]
    public void CanGenerateAuthToken()
    {
        //arrange
        var payload = new AuthPayload()
        {
            UserId = Guid.NewGuid(),
            Issuer = "Me",
            ExpirationDate = DateTimeTimeStemp.FromDateTime(DateTime.Today.AddDays(1)),
            RoleId = 1
        };
        
        //act
        var token = _generator.CreateToken(payload);

        //assert
        Assert.NotNull(token);
        Assert.False(string.IsNullOrWhiteSpace(token));
        
    }
    
    [Fact]
    public void CanReadGeneratedToken()
    {
        //arrange
        var payload = new AuthPayload()
        {
            UserId = Guid.NewGuid(),
            Issuer = "Me",
            RoleId = 2
        };
        var tokenContent = _generator.CreateToken(payload);
        
        //act
        var parseContent = _parser.GetPayloadFromToken<AuthPayload>(Token.Create(tokenContent).Value);
        
        Assert.True(parseContent.IsSuccess);
    }
}
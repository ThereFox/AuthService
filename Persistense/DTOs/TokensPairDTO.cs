using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Domain.ValueObjects;

namespace Persistense.DTOs;

public class TokensPairDTO
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthToken { get; set; }
    public string RefreshToken { get; set; }
    
    public UserDTO User { get; set; }
}

public static class Converters
{
    public static TokensPairDTO ToDTO(this Tokens self)
    {
        if (self == null) return null;

        return new TokensPairDTO()
        {
            Id = self.Id,
            CreatedAt = self.CreateDate,
            RefreshToken = self.TokensPair.RefreshToken.Token.Value,
            AuthToken = self.TokensPair.AuthToken.Token.Value
        };
    }

    public static Result<Tokens> ToDomain(this TokensPairDTO self)
    {
        var validateCommonAuthToken = Token.Create(self.AuthToken);
        var validateCommonRefreshToken = Token.Create(self.RefreshToken);

        if (validateCommonAuthToken.IsFailure || validateCommonRefreshToken.IsFailure)
        {
            return Result.Failure<Tokens>("Invalid input token");
        }
        
        var authToken = AuthorisationToken.Create( validateCommonAuthToken.Value );
        var refreshToken = RefreshToken.Create(validateCommonRefreshToken.Value);

        if (authToken.IsFailure || refreshToken.IsFailure)
        {
            return Result.Failure<Tokens>("Invalid input token content");
        }
        
        var pair = TokensPair.Create(authToken.Value, refreshToken.Value);

        if (pair.IsFailure)
        {
            return pair.ConvertFailure<Tokens>();
        }
        
        return Tokens.Create(self.Id, false, self.CreatedAt, pair.Value, null);
    }
}
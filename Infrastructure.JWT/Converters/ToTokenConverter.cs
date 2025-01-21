using AuntificationService.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Infrastructure.Tokens.JWT.Converters;

public static class ToTokenConverter
{
    public static Result<AuthorisationToken> ToAuthorisationToken(this string token)
    {
        var validateToken = Token.Create(token);

        if (validateToken.IsFailure)
        {
            return validateToken.ConvertFailure<AuthorisationToken>();
        }
        
        var authorisationToken = AuthorisationToken.Create(validateToken.Value);
        
        return authorisationToken;
    }
    public static Result<RefreshToken> ToRefreshToken(this string token)
    {
        var validateToken = Token.Create(token);

        if (validateToken.IsFailure)
        {
            return validateToken.ConvertFailure<RefreshToken>();
        }
        
        var authorisationToken = RefreshToken.Create(validateToken.Value);
        
        return authorisationToken;
    }
}
namespace WebAPI.Configurations.Services;

public record JWTConfig
(
    string HeaderName,
    string CookeyName,
    string Secret
);
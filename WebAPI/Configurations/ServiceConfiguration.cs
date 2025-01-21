using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Configurations.Services;

namespace WebAPI.Configurations
{
    public record ServiceConfiguration
    (
        DatabaseConfigInputObject MainDatabase,
        JWTConfig JWTConfig
    );
}

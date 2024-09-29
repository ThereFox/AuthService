using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tokens.Getter
{
    public static class TokensGetterDIRegister
    {
        public static IServiceCollection AddHttpTokenGetter(this IServiceCollection services, string headerName, string cookieName)
        {
            return services;
        }
    }
}

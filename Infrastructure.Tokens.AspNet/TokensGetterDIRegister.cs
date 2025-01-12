using Application.Interfaces.Getter;
using Application.Interfaces.TokenOnUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tokens.Getter
{
    public static class TokensGetterDIRegister
    {
        public static IServiceCollection AddHttpTokenGetter(this IServiceCollection services, string headerName, string cookieName)
        {
            services.AddTransient<ITokenGetter, HttpTokenGetter>(
                ex =>
                {
                    var accessor = ex.GetService<IHttpContextAccessor>();
                    return new HttpTokenGetter(accessor, headerName, cookieName);
                }
                );
            services.AddTransient<ITokenSetter, HttpTokenGetter>(
                ex =>
                {
                    var accessor = ex.GetService<IHttpContextAccessor>();
                    return new HttpTokenGetter(accessor, headerName, cookieName);
                }
            );
            
            return services;
        }
    }
}

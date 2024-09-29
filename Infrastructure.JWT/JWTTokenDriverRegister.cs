using Application.Interfaces;
using Infrastructure.Tokens.JWT.Driver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.JWT
{
    public static class JWTTokenDriverRegister
    {
        public static IServiceCollection AddJWTTokenDriver(this IServiceCollection services)
        {
            services.AddScoped<ITokenParser, TokenParser>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}

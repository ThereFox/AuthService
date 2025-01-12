using Application.Interfaces;
using Infrastructure.Tokens.JWT.Driver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Tokens.JWT.Encodings;
using Infrastructure.Tokens.JWT.Encodings.Realisation;
using Infrastructure.Tokens.JWT.JWTCreater;

namespace Infrastructure.Tokens.JWT
{
    public static class JWTTokenDriverRegister
    {
        public static IServiceCollection AddJWTTokenDriver(this IServiceCollection services, string jwtSecret)
        {
            services.AddScoped<ITokenParser, TokenParser>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddTransient<IEncodingReader, Base64Encoder>();
            services.AddScoped<JWTTokenGenerator>();
            services.AddScoped<JWTTokenParser>();
            services.AddScoped<JWTTokenSigner>(
                ex =>
                {
                    var encoder = new HMACSHA256();
                    return new JWTTokenSigner(encoder, jwtSecret);
                }
                );
            
            return services;
        }
    }
}

using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DI
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection collection)
        {
            collection.AddTransient<GetInfoByTokenUseCase>();
            collection.AddTransient<RefreshTokenUseCase>();
            collection.AddTransient<AuthentificateUseCase>();
            collection.AddTransient<LogOutUseCase>();
            collection.AddTransient<RegistrateUseCase>();

            return collection;
        }
    }
}

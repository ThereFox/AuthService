using Application.Stores;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DI
{
    public static class PersistenseRegister
    {
        public static IServiceCollection AddPersistense(this IServiceCollection collection)
        {
            collection.AddScoped<ITokenStore, TokenStore>();
            collection.AddScoped<IUserStore, UserStore>();

            return collection;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security;
using JWT.Services;

namespace JWT
{
    public class ObjectDI
    {
        public static void LoadInject(IServiceCollection services)
        {
            services.AddScoped<Ijwt, jwt>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

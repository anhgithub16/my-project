using Microsoft.Extensions.DependencyInjection;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service.api
{
    public class ObjectDi
    {
        public static void LoadInjection(IServiceCollection services)
        {
            services.AddScoped<Ijwt, jwt>();
        }
    }
}

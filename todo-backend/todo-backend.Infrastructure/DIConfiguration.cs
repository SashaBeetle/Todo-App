using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Infrastructure
{
    public static class DIConfiguration
    {
        private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<TodoDbContext>(options => 
                options
                    .UseNpgsql(configuration.GetConnectionString("TodoDatabase"))
            );
        }

        private static void RegisterServiceDependencies(this IServiceCollection services)
        {
            //services.AddScoped<ICatalogService, CatalogService>();
            //services.AddScoped<ICardService, CardService>();
        }
        public static void RegisterDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            RegisterDatabaseDependencies(services, configuration);
            RegisterServiceDependencies(services);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.Infrastructure.Repositories;
using todo_backend.Infrastructure.Services;

namespace todo_backend.Infrastructure
{
    public static class DIConfiguration
    {
        private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<TodoDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("TodoDatabase")));
        }

        private static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
        }
        public static void RegisterDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            RegisterDatabaseDependencies(services, configuration);
            RegisterServiceDependencies(services);
        }
    }
}
using Ecommerce.Application.Common.ConfigurationOptions;
using Ecommerce.Application.Common.Persistence;
using Ecommerce.Infrastructure.Persistence.Context;
using Ecommerce.Infrastructure.Persistence.Initialization;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Persistence
{
    internal static class Extensions
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<EcommerceDbContext>(options => options.UseSqlServer(appSettings.ConnectionStrings.EcommerceDbConnection));

            services.AddRepositories();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient<ApplicationDbInitializer>();
            services.AddTransient<ApplicationDbSeeder>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }
    }
}

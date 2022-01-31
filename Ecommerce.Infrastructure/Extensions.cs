using Ecommerce.Application.Common.ConfigurationOptions;
using Ecommerce.Infrastructure.FileStorage;
using Ecommerce.Infrastructure.Identity;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
        {
            services
                .AddIdentityFramework(appSettings)
                .AddFileStorage()
                .AddPersistence(appSettings);

            services.AddRazorPages();
            services.AddControllers();
            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".Ecommerce.Session";
            });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
        {
            builder.UseDeveloperExceptionPage();
            builder.UseMigrationsEndPoint();

            builder.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });

            builder.UseHttpsRedirection();
            builder.UseStaticFiles();

            builder.UseRouting();

            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseCookiePolicy();
            builder.UseSession();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            return builder;
        }

        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
                .InitializeAsync(cancellationToken);
        }
    }
}

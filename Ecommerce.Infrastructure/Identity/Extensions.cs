using Ecommerce.Application.Common.ConfigurationOptions;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Identity;
using Ecommerce.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ecommerce.Infrastructure.Identity
{
    internal static class Extensions
    {
        internal static IServiceCollection AddIdentityFramework(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<EcommerceDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Pages/Account/Login";
                options.SlidingExpiration = true;
            });

            services.AddExternalLogin(appSettings);

            return services;
        }

        private static IServiceCollection AddExternalLogin(this IServiceCollection services, AppSettings appSettings)
        {
            var authenBuilder = services.AddAuthentication();

            if (appSettings?.ExternalLogin?.Microsoft?.IsEnabled ?? false)
            {
                authenBuilder.AddMicrosoftAccount(options =>
                {
                    options.ClientId = appSettings.ExternalLogin.Microsoft.ClientId;
                    options.ClientSecret = appSettings.ExternalLogin.Microsoft.ClientSecret;
                });
            }

            if (appSettings?.ExternalLogin?.Google?.IsEnabled ?? false)
            {
                authenBuilder.AddGoogle(options =>
                {
                    options.ClientId = appSettings.ExternalLogin.Google.ClientId;
                    options.ClientSecret = appSettings.ExternalLogin.Google.ClientSecret;
                });
            }

            if (appSettings?.ExternalLogin?.Facebook?.IsEnabled ?? false)
            {
                authenBuilder.AddFacebook(options =>
                {
                    options.AppId = appSettings.ExternalLogin.Facebook.AppId;
                    options.AppSecret = appSettings.ExternalLogin.Facebook.AppSecret;
                });
            }

            return services;
        }
    }
}

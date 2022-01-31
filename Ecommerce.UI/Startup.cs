using Ecommerce.Application;
using Ecommerce.Application.Common.ConfigurationOptions;
using Ecommerce.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private AppSettings AppSettings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddApplication();
            services.AddInfrastructure(AppSettings);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.InitializeDatabasesAsync().GetAwaiter().GetResult();
            app.UseInfrastructure();
        }
    }
}

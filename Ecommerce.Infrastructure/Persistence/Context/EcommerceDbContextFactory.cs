using Ecommerce.Application.Common.ConfigurationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Ecommerce.Infrastructure.Persistence.Context
{
    public class EcommerceDbContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
    {
        public EcommerceDbContext CreateDbContext(string[] args)
        {
            ServiceCollection services = new(); 
            string basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Using `{basePath}` as the ContentRootPath");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .Build();

            services.AddSingleton(provider => configuration);
            services.Configure<AppSettings>(options => configuration.Bind(options));

            var settings = services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<AppSettings>>();

            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();
            optionsBuilder.UseSqlServer(settings.Value.ConnectionStrings.EcommerceDbConnection);

            return new EcommerceDbContext(optionsBuilder.Options);
        }
    }
}

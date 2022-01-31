using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Ecommerce.UI.Areas.Identity.IdentityHostingStartup))]
namespace Ecommerce.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
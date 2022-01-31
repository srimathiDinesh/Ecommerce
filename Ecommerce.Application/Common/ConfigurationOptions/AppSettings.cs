using Ecommerce.Application.Common.ConfigurationOptions.ExternalLogins;

namespace Ecommerce.Application.Common.ConfigurationOptions
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public ExternalLoginOptions ExternalLogin { get; set; }
    }
}

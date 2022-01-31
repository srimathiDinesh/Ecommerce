namespace Ecommerce.Application.Common.ConfigurationOptions.ExternalLogins
{
    public class ExternalLoginOptions
    {
        public MicrosoftOptions Microsoft { get; set; }
        public GoogleOptions Google { get; set; }
        public FacebookOptions Facebook { get; set; }
    }
}

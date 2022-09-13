namespace RestApi.Identity.Configuration
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpirationInSeconds { get; set; }
    }
}

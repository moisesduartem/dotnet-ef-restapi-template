using Microsoft.IdentityModel.Tokens;

namespace RestApi.Identity.Configuration
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int ExpirationInSeconds { get; set; }
    }
}

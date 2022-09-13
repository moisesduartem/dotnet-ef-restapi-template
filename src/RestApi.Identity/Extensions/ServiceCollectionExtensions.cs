using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestApi.Identity.Configuration;
using System.Text;

namespace RestApi.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonWebTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection jwtOptionsSection = configuration.GetSection(nameof(JwtOptions));

            var securityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    configuration.GetSection("JwtOptions:SecurityKey").Value
                ));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = 
                    jwtOptionsSection[nameof(JwtOptions.Issuer)];
                options.Audience = 
                    jwtOptionsSection[nameof(JwtOptions.Audience)];
                options.SigningCredentials = 
                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.ExpirationInSeconds = 
                    int.Parse(jwtOptionsSection[nameof(JwtOptions.ExpirationInSeconds)] ?? "0");
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }
    }
}

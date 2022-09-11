using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moisesduartem.WebApiTemplate.Application.V1.Options;
using Moisesduartem.WebApiTemplate.Application.V1.Services;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Handlers;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories;
using Moisesduartem.WebApiTemplate.Infra.Context;
using Moisesduartem.WebApiTemplate.Infra.Profiles;
using Moisesduartem.WebApiTemplate.Infra.Repositories;
using Moisesduartem.WebApiTemplate.Infra.Services;
using Serilog;
using System.Text;

namespace Moisesduartem.WebApiTemplate.Presentation.Builders
{
    public class DependencyBuilder
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;

        public DependencyBuilder(IConfiguration configuration, IServiceCollection services)
        {
            _configuration = configuration;
            _services = services;
        }

        public DependencyBuilder AddDatabase()
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                options.LogTo(Log.Logger.Information, LogLevel.Information, null);
            });

            return this;
        }

        public DependencyBuilder AddApiVersioning()
        {
            _services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("api-version"),
                    new UrlSegmentApiVersionReader()
                );
            });

            return this;
        }

        public DependencyBuilder AddAutoMapper()
        {
            _services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            return this;
        }

        public DependencyBuilder AddInjectedDependencies()
        {
            _services.AddScoped<IUserRepository, UserRepository>();
            
            _services.AddScoped<ITokenGenerationService, TokenGenerationService>();
            _services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            return this;
        }

        public DependencyBuilder AddFluentValidation()
        {
            _services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            _services.AddFluentValidationAutoValidation();
            return this;
        }

        public DependencyBuilder AddMediatR()
        {
            _services.AddMediatR(typeof(LoginQueryHandler).Assembly);
            return this;
        }
        
        public DependencyBuilder AddJwtAuthentication()
        {
            var authenticationSection = _configuration.GetSection("Authentication");

            var secretKey = Encoding.ASCII.GetBytes(authenticationSection.GetValue<string>("Secret"));

            _services.Configure<AuthenticationOptions>(authenticationSection);

            _services
                .AddHttpContextAccessor()
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }); ;

            return this;
        }
    }
}

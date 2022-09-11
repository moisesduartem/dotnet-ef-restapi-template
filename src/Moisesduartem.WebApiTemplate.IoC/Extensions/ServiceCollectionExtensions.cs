using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moisesduartem.WebApiTemplate.IoC.Builders;

namespace Moisesduartem.WebApiTemplate.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCrossCuttingConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            new DependencyBuilder(configurationManager, services)
                .AddAutoMapper()
                .AddMediatR()
                .AddFluentValidation()
                .AddJwtAuthentication()
                .AddInjectedDependencies();
        }
    }
}

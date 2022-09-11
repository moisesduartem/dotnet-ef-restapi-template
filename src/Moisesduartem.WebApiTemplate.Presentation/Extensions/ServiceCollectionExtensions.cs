using Moisesduartem.WebApiTemplate.Presentation.Builders;

namespace Moisesduartem.WebApiTemplate.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            new DependencyBuilder(configurationManager, services)
                .AddAutoMapper()
                .AddMediatR()
                .AddFluentValidation()
                .AddJwtAuthentication()
                .AddInjectedDependencies()
                .AddApiVersioning()
                .AddDatabase();
        }
    }
}

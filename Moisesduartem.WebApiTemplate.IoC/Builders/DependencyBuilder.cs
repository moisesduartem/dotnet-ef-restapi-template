using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Moisesduartem.WebApiTemplate.IoC.Builders
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

        public DependencyBuilder AddAutoMapper()
        {
            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return this;
        }

        public DependencyBuilder AddFluentValidation()
        {
            _services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            return this;
        }

        public DependencyBuilder AddMediatR()
        {
            _services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return this;
        }
    }
}

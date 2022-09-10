using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moisesduartem.WebApiTemplate.IoC.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moisesduartem.WebApiTemplate.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            new DependencyBuilder(configurationManager, services)
                .AddAutoMapper()
                .AddMediatR()
                .AddFluentValidation();
        }
    }
}

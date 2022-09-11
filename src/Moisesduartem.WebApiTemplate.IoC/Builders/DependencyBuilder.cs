﻿using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories;
using Moisesduartem.WebApiTemplate.Infra.Repositories;

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
            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return this;
        }

        public DependencyBuilder AddRepositories()
        {
            _services.AddScoped<IUserRepository, UserRepository>();

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
            _services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return this;
        }
    }
}
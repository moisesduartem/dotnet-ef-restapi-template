using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using RestApi.Extensions;
using RestApi.Filters;
using RestApi.Infra.Extensions;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg.WriteTo.Console());

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(typeof(ExceptionHandlerFilterAttribute));
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddEFCoreConfiguration();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddEmailSenderConfiguration(builder.Configuration);

builder.Services.AddDIConfiguration();

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddMediatorConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader()
    );
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenConfiguration();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
    });
}

app.UseCors(options => options
        .WithOrigins(builder.Configuration.GetSection("ApiClient:BaseUrl").Value)
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

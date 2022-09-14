using RestApi.Extensions;
using RestApi.Infra.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg.WriteTo.Console());

builder.Services.AddControllersConfiguration();

builder.Services.AddDIConfiguration();

builder.Services.AddIdentityAndEntityFramework(builder.Configuration);

builder.Services.AddEmailSenderConfiguration(builder.Configuration);

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddMediatorConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenConfiguration(applicationName: "RestApi");

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

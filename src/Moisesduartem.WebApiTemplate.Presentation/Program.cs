using Microsoft.OpenApi.Models;
using Moisesduartem.WebApiTemplate.IoC.Extensions;
using Moisesduartem.WebApiTemplate.Presentation.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg.WriteTo.Console());

builder.Services.AddCrossCuttingConfiguration(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Health Scheduler API" });
});

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

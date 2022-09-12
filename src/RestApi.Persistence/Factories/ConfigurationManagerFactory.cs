using Microsoft.Extensions.Configuration;

namespace RestApi.Persistence.Factories
{
    public static class ConfigurationManagerFactory
    {
        public static IConfiguration Get()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            return new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment}.json", optional: true)
               .Build();
        }
    }
}

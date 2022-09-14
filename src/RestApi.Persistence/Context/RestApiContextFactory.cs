using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RestApi.Persistence.Factories;

namespace RestApi.Persistence.Context
{
    internal class RestApiContextFactory : IDesignTimeDbContextFactory<RestApiContext>
    {
        public RestApiContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationManagerFactory.Get();

            var builder = new DbContextOptionsBuilder<RestApiContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new RestApiContext(builder.Options);
        }
    }
}

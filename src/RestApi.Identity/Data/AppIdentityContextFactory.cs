using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RestApi.Persistence.Factories;

namespace RestApi.Identity.Data
{
    internal class AppIdentityContextFactory : IDesignTimeDbContextFactory<AppIdentityContext>
    {
        public AppIdentityContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationManagerFactory.Get();

            var builder = new DbContextOptionsBuilder<AppIdentityContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new AppIdentityContext(builder.Options);
        }
    }
}

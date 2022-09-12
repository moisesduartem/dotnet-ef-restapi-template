using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RestApi.Persistence.Factories;

namespace RestApi.Persistence.Context
{
    public class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public AppContext CreateDbContext(string[] args)
        {
            var configuration = ConfigurationManagerFactory.Get();

            var builder = new DbContextOptionsBuilder<AppContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new AppContext(builder.Options);
        }
    }
}

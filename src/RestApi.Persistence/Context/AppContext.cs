using Microsoft.EntityFrameworkCore;
using RestApi.Domain.V1.Aggregates.Users.Entities;
using RestApi.Persistence.Mappings;

namespace RestApi.Persistence.Context
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}

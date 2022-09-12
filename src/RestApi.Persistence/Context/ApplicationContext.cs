using Microsoft.EntityFrameworkCore;
using RestApi.Domain.V1.Aggregates.Users.Entities;
using RestApi.Persistence.Mappings;

namespace RestApi.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}

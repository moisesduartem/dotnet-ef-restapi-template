using Microsoft.EntityFrameworkCore;
using RestApi.Domain.V1.Aggregates.Users.Entities;

namespace RestApi.Infra.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}

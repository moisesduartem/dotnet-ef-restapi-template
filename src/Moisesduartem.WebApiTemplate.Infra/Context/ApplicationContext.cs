using Microsoft.EntityFrameworkCore;
using Moisesduartem.WebApiTemplate.Domain.V1.Aggregates.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Infra.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}

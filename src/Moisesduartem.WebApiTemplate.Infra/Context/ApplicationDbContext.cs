using Microsoft.EntityFrameworkCore;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}

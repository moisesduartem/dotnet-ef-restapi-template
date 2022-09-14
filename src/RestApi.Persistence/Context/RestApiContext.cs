using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestApi.Application.Models;
using RestApi.Application.V1.Aggregates.Users.Constants;

namespace RestApi.Persistence.Context
{
    public class RestApiContext : IdentityDbContext<RestApiUser, RestApiRole, Guid>
    {
        public RestApiContext(DbContextOptions<RestApiContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureUserAndRoleId(builder);

            SeedAdminRole(builder);
        }

        private void ConfigureUserAndRoleId(ModelBuilder builder) {
            builder.Entity<RestApiUser>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            });

            builder.Entity<RestApiRole>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            });
        }

        private void SeedAdminRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole(AppRoles.Admin));
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestApi.Identity.Data
{
    public class ApplicationIdentityContext : IdentityDbContext
    {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) 
            : base(options)
        {
        }
    }
}

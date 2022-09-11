using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories;

namespace Moisesduartem.WebApiTemplate.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email)
        {
            var user = new User("Mr. User", "user@email.com", "username", "password123");
            return Task.FromResult<User?>(user);
        }
    }
}

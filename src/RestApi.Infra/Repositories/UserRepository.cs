using RestApi.Domain.V1.Aggregates.Users.Entities;
using RestApi.Domain.V1.Aggregates.Users.Repositories;

namespace RestApi.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = new User("Mr. User", "user@email.com", "username", "password123");
            return Task.FromResult<User?>(user);
        }
    }
}

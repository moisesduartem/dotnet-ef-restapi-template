using RestApi.Domain.V1.Shared;
using RestApi.Domain.V1.Aggregates.Users.Entities;

namespace RestApi.Domain.V1.Aggregates.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}

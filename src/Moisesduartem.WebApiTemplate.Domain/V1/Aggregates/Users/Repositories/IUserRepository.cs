using Moisesduartem.WebApiTemplate.Domain.V1.Shared;
using Moisesduartem.WebApiTemplate.Domain.V1.Aggregates.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Aggregates.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}

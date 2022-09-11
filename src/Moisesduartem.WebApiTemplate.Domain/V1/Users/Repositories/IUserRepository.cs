using Moisesduartem.WebApiTemplate.Domain.V1.Shared;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}

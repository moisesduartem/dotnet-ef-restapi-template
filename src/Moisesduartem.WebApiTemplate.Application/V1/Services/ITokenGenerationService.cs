using Moisesduartem.WebApiTemplate.Domain.V1.Users.Entities;

namespace Moisesduartem.WebApiTemplate.Application.V1.Services
{
    public interface ITokenGenerationService
    {
        string GenerateFor(User user);
    }
}

using RestApi.Domain.V1.Aggregates.Users.Entities;

namespace RestApi.Application.V1.Services
{
    public interface ITokenGenerationService
    {
        string GenerateFor(User user);
    }
}

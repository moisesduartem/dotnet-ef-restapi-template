using RestApi.Application.V1.Aggregates.Users.DTOs;

namespace RestApi.Application.V1.Services
{
    public interface IAuthenticatedUserService
    {
        LoggedUserDTO? GetLoggedUser();
    }
}

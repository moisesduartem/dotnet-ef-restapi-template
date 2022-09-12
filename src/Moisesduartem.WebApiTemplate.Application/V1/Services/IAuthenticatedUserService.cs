using Moisesduartem.WebApiTemplate.Application.V1.Aggregates.Users.DTOs;

namespace Moisesduartem.WebApiTemplate.Application.V1.Services
{
    public interface IAuthenticatedUserService
    {
        LoggedUserDTO? GetLoggedUser();
    }
}

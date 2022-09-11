using Moisesduartem.WebApiTemplate.Application.V1.Users.DTOs;

namespace Moisesduartem.WebApiTemplate.Application.V1.Services
{
    public interface IAuthenticatedUserService
    {
        LoggedUserDTO? GetLoggedUser();
    }
}

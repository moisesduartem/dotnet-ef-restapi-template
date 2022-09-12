using Microsoft.AspNetCore.Http;
using Moisesduartem.WebApiTemplate.Application.V1.Services;
using Moisesduartem.WebApiTemplate.Application.V1.Aggregates.Users.DTOs;
using System.Security.Claims;

namespace Moisesduartem.WebApiTemplate.Infra.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticatedUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public LoggedUserDTO? GetLoggedUser()
        {
            if (_contextAccessor.HttpContext is not null)
            {
                return CreateInstance(_contextAccessor.HttpContext.User);
            }

            return null;
        }

        private LoggedUserDTO CreateInstance(ClaimsPrincipal user)
        {
            return new LoggedUserDTO
            {
                Id = user.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = user.FindFirstValue(ClaimTypes.Name),
                Email = user.FindFirstValue(ClaimTypes.Email),
                Role = user.FindFirstValue(ClaimTypes.Role)
            };
        }
    }
}

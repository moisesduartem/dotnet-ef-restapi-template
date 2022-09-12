using Microsoft.AspNetCore.Http;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using System.Security.Claims;

namespace RestApi.Infra.Services
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

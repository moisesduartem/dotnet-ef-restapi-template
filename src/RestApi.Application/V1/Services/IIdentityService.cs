using Microsoft.AspNetCore.Identity;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Shared;

namespace RestApi.Application.V1.Services
{
    public interface IIdentityService
    {
        Task<Result> ConfirmEmailAsync(IdentityUser user, string token);
        Task<IdentityUser> FindUserByEmailAsync(string email);
        Task<Result> ForgotPasswordAsync(IdentityUser user, CancellationToken cancellationToken);
        Task<LoggedUserDTO?> GetLoggedUserAsync();
        Task<LoginDTO> LoginAsync(LoginQuery query);
        Task<Result> ResetPasswordAsync(IdentityUser user, string token, string password);
        Task<Result> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);
    }
}

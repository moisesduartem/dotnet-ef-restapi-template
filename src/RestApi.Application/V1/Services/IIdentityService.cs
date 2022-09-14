using Microsoft.AspNetCore.Identity;
using RestApi.Application.Models;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Shared;

namespace RestApi.Application.V1.Services
{
    public interface IIdentityService
    {
        Task<Result> ConfirmEmailAsync(RestApiUser user, string token);
        Task<RestApiUser> FindUserByEmailAsync(string email);
        Task<Result> ForgotPasswordAsync(RestApiUser user, CancellationToken cancellationToken);
        Task<LoggedUserDTO?> GetLoggedUserAsync();
        Task<LoginDTO> LoginAsync(LoginQuery query);
        Task<Result> ResetPasswordAsync(RestApiUser user, string token, string password);
        Task<Result> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);
    }
}

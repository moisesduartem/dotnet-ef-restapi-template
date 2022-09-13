using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Shared;

namespace RestApi.Application.V1.Services
{
    public interface IIdentityService
    {
        Task<Result> ConfirmEmailAsync(ConfirmEmailCommand command);
        Task<Result> ForgotPasswordAsync(ForgotPasswordCommand command, CancellationToken cancellationToken);
        Task<LoggedUserDTO?> GetLoggedUserAsync();
        Task<LoginDTO> LoginAsync(LoginQuery query);
        Task<Result> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);
    }
}

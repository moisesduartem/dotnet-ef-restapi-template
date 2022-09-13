using Microsoft.AspNetCore.Identity;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.DTOs;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Shared;

namespace RestApi.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public Task<IResult<LoginDTO>> Login(LoginQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> RegisterAsync(RegisterUserCommand command)
        {
            var identityUser = new IdentityUser
            {
                UserName = command.Username,
                Email = command.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, command.Password);

            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
                return Result.Create().Ok();
            }

            IList<string> errors = result.Errors.Select(x => x.Description).ToList();

            return Result.Create().Error(errors);
        }
    }
}

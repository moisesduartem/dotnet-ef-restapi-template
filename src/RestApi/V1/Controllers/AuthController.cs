using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.V1.Aggregates.Users.Commands;
using RestApi.Application.V1.Aggregates.Users.Constants;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Services;

namespace RestApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            var result = await _identityService.LoginAsync(query);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(command, cancellationToken);

            if (result.Success)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest(result);
        }
        
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            var result = await _identityService.ConfirmEmailAsync(command);

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result);
        }
        
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _identityService.ForgotPasswordAsync(command, cancellationToken);

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result);
        }
        
        [HttpPatch("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await _identityService.ResetPasswordAsync(command);

            if (result.Success)
            {
                return NoContent();
            }

            return BadRequest(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _identityService.GetLoggedUserAsync();

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest(user);
        }

        [HttpGet("admin")]
        [Authorize(Roles = AppRoles.Admin)]
        public IActionResult VerifyAdminUser()
        {
            return NoContent();
        }
        
        [HttpGet("user")]
        [Authorize]
        public IActionResult VerifyRegularUser()
        {
            return NoContent();
        }
    }
}

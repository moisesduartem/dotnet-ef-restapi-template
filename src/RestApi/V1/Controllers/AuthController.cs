using MediatR;
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
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public AuthController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _identityService.RegisterAsync(command);

            if (result.Success)
            {
                return StatusCode(StatusCodes.Status201Created);
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

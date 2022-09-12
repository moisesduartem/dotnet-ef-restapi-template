using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.V1.Services;
using RestApi.Application.V1.Aggregates.Users.Queries;
using RestApi.Application.V1.Aggregates.Users.Commands;

namespace RestApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IIdentityService _identityService;

        public AuthController(IMediator mediator, IAuthenticatedUserService userService, IIdentityService identityService)
        {
            _mediator = mediator;
            _authenticatedUserService = userService;
            _identityService = identityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsValid)
            {
                return Ok(result.Value);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(command);

            if (result.IsValid)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            var user = _authenticatedUserService.GetLoggedUser();

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest(user);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyAdminRole()
        {
            return NoContent();
        }
        
        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public IActionResult VerifyUserRole()
        {
            return NoContent();
        }
    }
}

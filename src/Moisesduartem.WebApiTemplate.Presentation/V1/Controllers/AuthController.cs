using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moisesduartem.WebApiTemplate.Application.V1.Services;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Queries;

namespace Moisesduartem.WebApiTemplate.Presentation.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AuthController(IMediator mediator, IAuthenticatedUserService userService)
        {
            _mediator = mediator;
            _authenticatedUserService = userService;
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

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moisesduartem.WebApiTemplate.Application.V1.Users.Queries;
using Moisesduartem.WebApiTemplate.Domain.V1.Users.Repositories;

namespace Moisesduartem.WebApiTemplate.Presentation.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query, CancellationToken cancellationToken)
        {
            throw new Exception();
            var result = await _mediator.Send(query, cancellationToken);
            
            if (result.IsValid)
            {
                return Ok(result.Value);
            }

            return BadRequest(result);
        }
    }
}

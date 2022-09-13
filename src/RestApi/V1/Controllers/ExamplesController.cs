using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.V1.Aggregates.Examples;
using RestApi.Application.V1.Aggregates.Users.Queries;

namespace RestApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/examples")]
    public class ExamplesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamplesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(
                new ExampleQuery { Id = Guid.NewGuid().ToString() }, 
                cancellationToken
            );
            
            return Ok(dto);
        }
    }
}

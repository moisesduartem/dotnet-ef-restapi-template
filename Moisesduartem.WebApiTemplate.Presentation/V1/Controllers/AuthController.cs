using Microsoft.AspNetCore.Mvc;

namespace Moisesduartem.WebApiTemplate.Presentation.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> signIn()
        {
            return await Task.Run(() => Ok("hello world!"));
        }
    }
}

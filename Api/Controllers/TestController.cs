using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> AnonymousGet()
        {
            return Ok("Can access anonymous data.");
        }

        [HttpGet("authorize")]
        public ActionResult<string> AuthorizeGet()
        {
            return Ok("Can access authorized data.");
        }
    }
}

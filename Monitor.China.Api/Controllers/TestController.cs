using Microsoft.AspNetCore.Mvc;

namespace Monitor.China.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Monitor.China.Api is running.");
        }
    }
}

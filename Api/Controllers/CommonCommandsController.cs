using Application.MonitorApis.CommonCommands.GetMonitorConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonCommandsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("GetMonitorConfiguration")]
        public async Task<ActionResult<GetMonitorConfigurationCommandResp>> GetMonitorConfiguration()
        {
            return Ok(await Mediator.Send(new GetMonitorConfigurationCommand()));
        }
    }
}

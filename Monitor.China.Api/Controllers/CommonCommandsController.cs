using Microsoft.AspNetCore.Mvc;
using Monitor.API.Common.Commands.Configuration;
using Monitor.China.Api.MonitorApis.Commands;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Common/Commands")]
    [ApiController]
    public class CommonCommandsController : ControllerBase
    {
        private readonly CommandFactory commandFactory;

        public CommonCommandsController(CommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        [HttpPost("GetMonitorConfiguration")]
        public async Task<ActionResult<MonitorConfigurationInfo>> GetMonitorConfiguration()
        {
            var configurationCommand = commandFactory.CreateConfigurationCommand();
            return Ok(await configurationCommand.GetMonitorConfigurationAsync());
        }
    }
}

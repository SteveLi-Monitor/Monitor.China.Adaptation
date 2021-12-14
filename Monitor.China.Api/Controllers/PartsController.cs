using Microsoft.AspNetCore.Mvc;
using Monitor.API.Infrastructure;
using Monitor.API.Inventory.Commands.Parts;
using Monitor.China.Api.Dtos;
using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Inventory/Parts")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly GenericQuery<PartDto> query;
        private readonly GenericCommand command;

        public PartsController(QueryFactory queryFactory, CommandFactory commandFactory)
        {
            query = queryFactory.Create<PartDto>();
            command = commandFactory.Create();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartDto>>> Get(string options)
        {
            return Ok(await query.GetAsync(options));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<EntityCommandResponse>> Create(CreatePart createPart)
        {
            return Ok(await command.SendAsync(createPart));
        }
    }
}

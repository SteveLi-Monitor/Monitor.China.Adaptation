using Dapper;
using Microsoft.AspNetCore.Mvc;
using Monitor.API.Infrastructure;
using Monitor.API.Inventory.Commands.Parts;
using Monitor.China.Api.Bootstrap;
using Monitor.China.Api.Dtos;
using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Queries;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Inventory/Parts")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly GenericQuery<PartDto> query;
        private readonly GenericCommand command;
        private readonly IDbConnection dbConnection;

        public PartsController(
            QueryFactory queryFactory,
            CommandFactory commandFactory,
            SaDbConnection saDbConnection)
        {
            query = queryFactory.Create<PartDto>();
            command = commandFactory.Create();
            dbConnection = saDbConnection.DbConnection;
        }

        [HttpGet]
        public Task<IEnumerable<PartDto>> Get(string options)
        {
            return query.GetAsync(options);
        }

        [HttpGet("{id}")]
        public Task<PartDto> Get(long id)
        {
            return query.GetByIdAsync(id);
        }

        [HttpPost("Create")]
        public Task<EntityCommandResponse> Create(CreateDto createDto)
        {
            return command.SendAsync(
                new CreatePart
                {
                    PartNumber = createDto.PartNumber,
                    Description = createDto.Description,
                    StandardUnitId = createDto.StandardUnitId
                });
        }

        [HttpGet("db")]
        public Task<IEnumerable<PartDto>> Get()
        {
            return dbConnection.QueryAsync<PartDto>("select Part.Id, Part.PartNumber from monitor.Part");
        }

        public class CreateDto
        {
            public string PartNumber { get; set; }

            public string Description { get; set; }

            public long StandardUnitId { get; set; }
        }
    }
}

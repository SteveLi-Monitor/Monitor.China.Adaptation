using Microsoft.AspNetCore.Mvc;
using Monitor.API.Infrastructure;
using Monitor.API.Sales.Commands.CustomerOrders;
using Monitor.China.Api.Dtos;
using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Sales/CustomerOrders")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly GenericQuery<CustomerOrderDto> query;
        private readonly GenericCommand command;

        public CustomerOrdersController(QueryFactory queryFactory, CommandFactory commandFactory)
        {
            query = queryFactory.Create<CustomerOrderDto>();
            command = commandFactory.Create();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOrderDto>>> Get(string options)
        {
            return Ok(await query.GetAsync(options));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrderDto>> GetById(long id)
        {
            return Ok(await query.GetByIdAsync(id));
        }

        [HttpPost("Create")]
        public async Task<ActionResult<EntityCommandResponse>> Create(CreateCustomerOrder createCustomerOrder)
        {
            return Ok(await command.SendAsync(createCustomerOrder));
        }

        [HttpPost("SetProperties")]
        public async Task<ActionResult<EntityCommandResponse>> SetProperties(SetPropertiesCustomerOrder setPropertiesCustomerOrder)
        {
            return Ok(await command.SendAsync(setPropertiesCustomerOrder));
        }
    }
}

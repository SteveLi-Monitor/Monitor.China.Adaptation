using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Dtos;
using Monitor.China.Api.MonitorApis.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Sales/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly GenericQuery<CustomerDto> query;

        public CustomersController(QueryFactory queryFactory)
        {
            query = queryFactory.Create<CustomerDto>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get(string options)
        {
            return Ok(await query.GetAsync(options));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(long id)
        {
            return Ok(await query.GetByIdAsync(id));
        }
    }
}

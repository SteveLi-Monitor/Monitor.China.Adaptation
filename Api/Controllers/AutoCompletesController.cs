using Application.AutoCompletes.Commands.Customer;
using Application.AutoCompletes.Commands.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutoCompletesController : ApiControllerBase
    {
        [HttpPost("Customer")]
        public async Task<ActionResult<CustomerCommandResp>> Customer(CustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Part")]
        public async Task<ActionResult<PartCommandResp>> Part(PartCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

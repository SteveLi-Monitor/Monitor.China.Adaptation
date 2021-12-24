using Application.UserRoles.Commands.Create;
using Application.UserRoles.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRolesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllQueryResp>> Get()
        {
            return Ok(await Mediator.Send(new GetAllQuery()));
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

using Application.Users.Commands.SignIn;
using Application.Users.Commands.UpdateUserRoleAndUiComponents;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetById;
using Application.Users.Queries.GetUiComponentsById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllQueryResp>> Get()
        {
            return Ok(await Mediator.Send(new GetAllQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdQueryResp>> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetByIdQuery { Id = id }));
        }

        [HttpGet("UiComponents/{id}")]
        public async Task<ActionResult<GetUiComponentsByIdQueryResp>> GetUiComponentsById(string id)
        {
            return Ok(await Mediator.Send(new GetUiComponentsByIdQuery { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInCommandResp>> SignIn(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("UpdateUserRoleAndUiComponents")]
        public async Task<ActionResult> UpdateUserRoleAndUiComponents(UpdateUserRoleAndUiComponentsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

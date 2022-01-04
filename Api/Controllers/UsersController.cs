using Application.Users.Commands.SignIn;
using Application.Users.Queries.GetAll;
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
            //return Ok(await Mediator.Send(new GetAllQuery()));
            return Ok(new GetAllQueryResp());
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInCommandResp>> SignIn(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

using Application.Users.Commands.SignIn;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInCommandResp>> SignIn(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

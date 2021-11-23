using Application.Users.Commands.SignIn;
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
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult<SignInCommandResp>> SignIn(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

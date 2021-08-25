using Application.Users.Commands.SignIn;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignInAsync(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}

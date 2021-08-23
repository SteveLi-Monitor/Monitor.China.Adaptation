using Application.Common.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public UsersController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("signin")]
        public ActionResult<string> SignIn()
        {
            var option = configuration.GetSection(nameof(JwtTokenOption)).Get<JwtTokenOption>();
            return new JwtTokenBuilder(option)
                .AddClaim(new Claim("Username", "API01"))
                .WriteToken();
        }
    }
}

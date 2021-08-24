using Application.Common;
using Application.Common.Jwt;
using Application.Common.MonitorApi;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private ApplicationUser applicationUser;
        private readonly MonitorApiService monitorApiService;

        public UsersController(
            IConfiguration configuration,
            ApplicationUser applicationUser,
            MonitorApiService monitorApiService)
        {
            this.configuration = configuration;
            this.applicationUser = applicationUser;
            this.monitorApiService = monitorApiService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignInAsync(MonitorApiUser monitorApiUser)
        {
            applicationUser.Username = monitorApiUser.Username;
            applicationUser.Password = monitorApiUser.Password;
            applicationUser.CompanyNumber = monitorApiUser.CompanyNumber;
            applicationUser.LanguageCode = monitorApiUser.LanguageCode;

            await monitorApiService.SignInAsync();

            var option = configuration.GetSection(nameof(JwtTokenOption)).Get<JwtTokenOption>();
            return new JwtTokenBuilder(option)
                .AddClaim(
                    new Claim(nameof(ApplicationUser),
                    JsonConvert.SerializeObject(applicationUser)))
                .WriteToken();
        }
    }
}

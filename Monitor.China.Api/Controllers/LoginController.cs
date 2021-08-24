using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
using System;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApiTransaction apiTransaction;

        public LoginController(ApiTransaction apiTransaction)
        {
            this.apiTransaction = apiTransaction;
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            try
            {
                await apiTransaction.CreateAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"{e.GetType().Name}: {e.Message}");
            }
        }

        [HttpPost("WithCertificate")]
        public async Task<IActionResult> LoginWithCertificate()
        {
            try
            {
                await apiTransaction.CreateWithCertificateAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"{e.GetType().Name}: {e.Message}");
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
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
            return Ok(await apiTransaction.CreateAsync());
        }

        [HttpPost("WithCertificate")]
        public async Task<IActionResult> LoginWithCertificate()
        {
            return Ok(await apiTransaction.CreateWithCertificateAsync());
        }

    }
}

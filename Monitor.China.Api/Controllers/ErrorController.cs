using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace Monitor.China.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature?.Error;
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = $"{exception.GetType().Name}: {exception.Message}",
                Detail = exception.StackTrace
            };

            Log.Error(exception, $"Unhandled exception for request: {feature?.Path}.");

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }
    }
}

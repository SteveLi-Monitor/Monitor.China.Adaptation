using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;

namespace Api.Controllers
{
    [ApiController]
    [OpenApiIgnore]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ValidationException e)
            {
                return BadRequest(e.Errors);
            }

            if (context.Error is MonitorApiException monitorApiEx)
            {
                return Problem(
                    detail: monitorApiEx.ProblemDetails.Detail,
                    title: monitorApiEx.ProblemDetails.Title);
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ValidationException e)
            {
                return BadRequest(e.Errors);
            }

            if (context.Error is MonitorApiException monitorApiEx)
            {
                return Problem(
                    detail: monitorApiEx.ProblemDetails.Detail,
                    title: monitorApiEx.ProblemDetails.Title);
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}

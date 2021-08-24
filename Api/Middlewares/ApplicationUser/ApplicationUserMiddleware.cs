using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middlewares.ApplicationUser
{
    public class ApplicationUserMiddleware
    {
        private readonly RequestDelegate next;

        public ApplicationUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, Application.Common.ApplicationUser applicationUser)
        {
            if (context.Request.Path.StartsWithSegments(PathString.FromUriComponent("/api")))
            {
                var claim = context.User.Claims.FirstOrDefault(x => x.Type == nameof(ApplicationUser));

                if (claim != null)
                {
                    applicationUser = DeserializeApplicationUser(claim.Value);
                }
            }

            await next(context);
        }

        private Application.Common.ApplicationUser DeserializeApplicationUser(string value)
        {
            try
            {
                var applicationUser = JsonConvert.DeserializeObject<Application.Common.ApplicationUser>(value);
                applicationUser.Guard(nameof(applicationUser));
                applicationUser.Guard();
                return applicationUser;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"{nameof(Application.Common.ApplicationUser)} is invalid: {value}.", e);
            }
        }
    }
}

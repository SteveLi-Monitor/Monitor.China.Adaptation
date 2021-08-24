using Microsoft.AspNetCore.Builder;

namespace Api.Middlewares.ApplicationUser
{
    public static class ApplicationUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationUser(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationUserMiddleware>();
        }
    }
}

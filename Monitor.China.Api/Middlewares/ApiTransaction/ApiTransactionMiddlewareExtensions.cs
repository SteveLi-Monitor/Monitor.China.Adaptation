using Microsoft.AspNetCore.Builder;

namespace Monitor.China.Api.Middlewares.ApiTransaction
{
    public static class ApiTransactionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiTransaction(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiTransactionMiddleware>();
        }
    }
}

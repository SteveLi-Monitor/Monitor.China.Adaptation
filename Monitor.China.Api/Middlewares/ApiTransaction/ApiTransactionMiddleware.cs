using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Monitor.China.Api.Bootstrap;
using Monitor.China.Api.Exceptions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Monitor.China.Api.Middlewares.ApiTransaction
{
    public class ApiTransactionMiddleware
    {
        private readonly RequestDelegate next;

        public ApiTransactionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, Bootstrap.ApiTransaction apiTransaction)
        {
            var header = context.Request.Headers[Constants.ApiSettingHeader];

            if (StringValues.IsNullOrEmpty(header))
            {
                throw new RequestHeaderNotFoundException(Constants.ApiSettingHeader);
            }

            apiTransaction.ApiSetting = JsonConvert.DeserializeObject<ApiSetting>(header);

            await next(context);
        }
    }
}

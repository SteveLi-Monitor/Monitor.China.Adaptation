using Domain;
using Domain.Common;
using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Monitor.China.Api.Exceptions;
using Newtonsoft.Json;
using System;
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
            if (context.Request.Path.StartsWithSegments(PathString.FromUriComponent("/api")))
            {
                var header = context.Request.Headers[Constants.MonitorApiUserHeader];

                if (StringValues.IsNullOrEmpty(header))
                {
                    throw new RequestHeaderNotFoundException(Constants.MonitorApiUserHeader);
                }

                apiTransaction.MonitorApiUser = DeserializeMonitorApiUser(header);
            }

            await next(context);
        }

        private MonitorApiUser DeserializeMonitorApiUser(string value)
        {
            try
            {
                var monitorApiUser = JsonConvert.DeserializeObject<MonitorApiUser>(value);
                monitorApiUser.Guard(nameof(monitorApiUser));
                monitorApiUser.Guard();
                return monitorApiUser;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"{nameof(MonitorApiUser)} is invalid: {value}.", e);
            }
        }
    }
}

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
                var header = context.Request.Headers[Constants.MonitorApiSettingHeader];

                if (StringValues.IsNullOrEmpty(header))
                {
                    throw new RequestHeaderNotFoundException(Constants.MonitorApiSettingHeader);
                }

                apiTransaction.MonitorApiSetting = DeserializeMonitorApiSetting(header);
            }

            await next(context);
        }

        private MonitorApiSetting DeserializeMonitorApiSetting(string value)
        {
            try
            {
                var monitorApiSetting = JsonConvert.DeserializeObject<MonitorApiSetting>(value);
                monitorApiSetting.Guard(nameof(monitorApiSetting));
                monitorApiSetting.Guard();
                return monitorApiSetting;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"{nameof(MonitorApiSetting)} is invalid: {value}.", e);
            }
        }
    }
}

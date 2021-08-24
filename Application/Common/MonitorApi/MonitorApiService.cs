using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Application.Common.MonitorApi
{
    public class MonitorApiService
    {
        public MonitorApiService(HttpClient httpClient, IConfiguration configuration)
        {
            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();

            monitorApiServiceSetting.Guard(nameof(monitorApiServiceSetting));
            monitorApiServiceSetting.Guard();

            httpClient.BaseAddress = new Uri(monitorApiServiceSetting.ServiceAddress);
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}

using Domain;
using Domain.Common;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Application.Common.MonitorApi
{
    public class MonitorApiService
    {
        public MonitorApiService(
            HttpClient httpClient,
            IConfiguration configuration,
            ApplicationUser applicationUser)
        {
            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();
            monitorApiServiceSetting.Guard(nameof(monitorApiServiceSetting));
            monitorApiServiceSetting.Guard();

            var monitorServerSetting = configuration
                .GetSection(nameof(MonitorServerSetting)).Get<MonitorServerSetting>();
            monitorServerSetting.Guard(nameof(monitorServerSetting));
            monitorServerSetting.Guard();

            applicationUser.Guard(nameof(applicationUser));
            applicationUser.Guard();

            httpClient.BaseAddress = new Uri(monitorApiServiceSetting.ServiceAddress);

            httpClient.DefaultRequestHeaders.Add(
                Constants.MonitorApiSettingHeader,
                JsonConvert.SerializeObject(new MonitorApiSetting
                {
                    MonitorServerSetting = monitorServerSetting,
                    MonitorApiUser = applicationUser
                }));

            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}

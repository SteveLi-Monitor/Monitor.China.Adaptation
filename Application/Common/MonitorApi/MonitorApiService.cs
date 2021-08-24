using Domain;
using Domain.Common;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Common.MonitorApi
{
    public class MonitorApiService
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationUser applicationUser;
        private readonly MonitorServerSetting monitorServerSetting;

        public MonitorApiService(
            HttpClient httpClient,
            IConfiguration configuration,
            ApplicationUser applicationUser)
        {
            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();
            monitorApiServiceSetting.Guard(nameof(monitorApiServiceSetting));
            monitorApiServiceSetting.Guard();

            monitorServerSetting = configuration
                .GetSection(nameof(MonitorServerSetting)).Get<MonitorServerSetting>();
            monitorServerSetting.Guard(nameof(monitorServerSetting));
            monitorServerSetting.Guard();

            httpClient.BaseAddress = new Uri(monitorApiServiceSetting.ServiceAddress);

            HttpClient = httpClient;
            this.configuration = configuration;
            this.applicationUser = applicationUser;
        }

        public HttpClient HttpClient { get; }

        public async Task SignInAsync()
        {
            SetMonitorApiSettingHeader();

            using var response = await HttpClient.PostAsync("/api/Login", null);
            response.EnsureSuccessStatusCode();
        }

        private void SetMonitorApiSettingHeader()
        {
            applicationUser.Guard(nameof(applicationUser));
            applicationUser.Guard();

            HttpClient.DefaultRequestHeaders.Add(
                Constants.MonitorApiSettingHeader,
                JsonConvert.SerializeObject(new MonitorApiSetting
                {
                    MonitorServerSetting = monitorServerSetting,
                    MonitorApiUser = applicationUser
                }));
        }
    }
}

using Application.Common;
using Application.Common.Settings;
using Domain;
using Domain.Common;
using Domain.Dtos;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Application.MonitorApis
{
    public class MonitorApiService
    {
        private readonly ApplicationUser applicationUser;
        private readonly ApplicationSetting applicationSetting;

        public MonitorApiService(
            HttpClient httpClient,
            IConfiguration configuration,
            ApplicationUser applicationUser)
        {
            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();

            applicationSetting = configuration
                .GetSection(nameof(ApplicationSetting)).Get<ApplicationSetting>();

            httpClient.BaseAddress = new Uri(monitorApiServiceSetting.ServiceAddress);
            httpClient.Timeout = TimeSpan.FromSeconds(5);

            HttpClient = httpClient;
            this.applicationUser = applicationUser;
        }

        public HttpClient HttpClient { get; }

        public async Task<LoginDto.LoginResp> SignInAsync()
        {
            using var request = CreateRequest(HttpMethod.Post, "/api/Login");
            request.Content = JsonContent.Create(
                new LoginDto.LoginReq
                {
                    Identifier = applicationSetting.ExtraFieldIdentifiers.PersonApiUserName
                });

            using var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoginDto.LoginResp>();
        }

        private HttpRequestMessage CreateRequest(HttpMethod httpMethod, string url)
        {
            applicationUser.Guard(nameof(applicationUser));
            applicationUser.Guard();

            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add(
                    Constants.MonitorApiUserHeader,
                    JsonConvert.SerializeObject(new MonitorApiUser
                    {
                        ApiUsername = applicationUser.ApiUsername,
                        Password = applicationUser.Password,
                        LanguageCode = applicationUser.LanguageCode,
                        CompanyNumber = applicationUser.CompanyNumber,
                    }));
            return request;
        }
    }
}

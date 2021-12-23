using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Settings;
using Domain;
using Domain.Common;
using Domain.Dtos;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
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
            await EnsureSuccessResponse(response);

            return await response.Content.ReadFromJsonAsync<LoginDto.LoginResp>();
        }

        #region AutoCompletesController

        private const string autoCompletesUrl = "/api/AutoCompletes";

        public async Task<AutoCompletesDto.PartResp> AutoCompleteOfPart(AutoCompletesDto.PartReq partReq)
        {
            using var request = CreateRequest(
                HttpMethod.Post,
                $"{autoCompletesUrl}/Part");
            request.Content = JsonContent.Create(partReq);

            using var response = await HttpClient.SendAsync(request);
            await EnsureSuccessResponse(response);

            return await response.Content.ReadFromJsonAsync<AutoCompletesDto.PartResp>();
        }

        #endregion

        #region CommonCommandsController

        private const string commonCommandsUrl = "/api/Common/Commands";

        public async Task<CommonCommandsDto.GetMonitorConfigurationResp> GetMonitorConfiguration()
        {
            using var request = CreateRequestWithDefaultApiUser(
                HttpMethod.Post,
                $"{commonCommandsUrl}/GetMonitorConfiguration");

            using var response = await HttpClient.SendAsync(request);
            await EnsureSuccessResponse(response);

            return await response.Content.ReadFromJsonAsync<CommonCommandsDto.GetMonitorConfigurationResp>();
        }

        #endregion CommonCommandsController

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

        private HttpRequestMessage CreateRequestWithDefaultApiUser(HttpMethod httpMethod, string url)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add(
                    Constants.MonitorApiUserHeader,
                    JsonConvert.SerializeObject(new MonitorApiUser
                    {
                        ApiUsername = applicationSetting.DefaultApiUser.ApiUsername,
                        Password = applicationSetting.DefaultApiUser.Password,
                        LanguageCode = applicationSetting.DefaultApiUser.LanguageCode,
                        CompanyNumber = applicationSetting.DefaultApiUser.CompanyNumber,
                    }));
            return request;
        }

        private async Task EnsureSuccessResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new MonitorApiException(await response.Content.ReadFromJsonAsync<ProblemDetails>());
            }
        }
    }
}

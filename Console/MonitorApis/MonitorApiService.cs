using Console.Exceptions;
using Domain;
using Domain.Common;
using Domain.Dtos;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Console.MonitorApis
{
    public class MonitorApiService
    {
        public MonitorApiService(IConfiguration configuration)
        {
            var monitorApiServiceSetting = configuration
                .GetSection(nameof(MonitorApiServiceSetting)).Get<MonitorApiServiceSetting>();
            monitorApiServiceSetting.Guard(nameof(monitorApiServiceSetting));
            monitorApiServiceSetting.Guard();

            var monitorApiUser = configuration
                .GetSection(nameof(MonitorApiUser)).Get<MonitorApiUser>();
            monitorApiUser.Guard(nameof(monitorApiUser));
            monitorApiUser.Guard();

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(monitorApiServiceSetting.ServiceAddress),
                Timeout = TimeSpan.FromSeconds(5)
            };

            MonitorApiUser = monitorApiUser;
        }

        public HttpClient HttpClient { get; }

        public MonitorApiUser MonitorApiUser { get; }

        #region PartsController

        private const string partsUrl = "/api/Inventory/Parts";

        public async Task<PartsDto.Part> GetPartByPartNumber(string partNumber)
        {
            partNumber.Guard(nameof(partNumber));

            using var request = CreateRequest(
                HttpMethod.Get,
                $"{partsUrl}?options=$filter=PartNumber eq '{partNumber}'");

            using var response = await HttpClient.SendAsync(request);
            await EnsureSuccessResponse(response);

            var parts = await response.Content.ReadFromJsonAsync<IEnumerable<PartsDto.Part>>();
            return parts.SingleOrDefault();
        }

        #endregion PartsController

        private HttpRequestMessage CreateRequest(HttpMethod httpMethod, string url)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add(
                    Constants.MonitorApiUserHeader,
                    JsonConvert.SerializeObject(new MonitorApiUser
                    {
                        ApiUsername = MonitorApiUser.ApiUsername,
                        Password = MonitorApiUser.Password,
                        LanguageCode = MonitorApiUser.LanguageCode,
                        CompanyNumber = MonitorApiUser.CompanyNumber,
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

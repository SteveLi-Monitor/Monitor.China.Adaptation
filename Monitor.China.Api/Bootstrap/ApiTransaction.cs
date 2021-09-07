using Domain.Common;
using Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Monitor.API.Client;
using System.IO;
using System.Threading.Tasks;

namespace Monitor.China.Api.Bootstrap
{
    public class ApiTransaction
    {
        private readonly IHttpTransactionService httpTransactionService;
        private readonly MonitorServerSetting monitorServerSetting;

        public ApiTransaction(IHttpTransactionService httpTransactionService, IConfiguration configuration)
        {
            this.httpTransactionService = httpTransactionService;
            monitorServerSetting = configuration.GetSection(nameof(MonitorServerSetting))
                .Get<MonitorServerSetting>();
        }

        public MonitorApiUser MonitorApiUser { get; set; }

        public Task<IHttpTransaction> CreateAsync()
        {
            return CreateAsync(false);
        }

        public Task<IHttpTransaction> CreateWithCertificateAsync()
        {
            return CreateAsync(true);
        }

        private Task<IHttpTransaction> CreateAsync(bool setCertificate)
        {
            MonitorApiUser.Guard(nameof(MonitorApiUser));
            MonitorApiUser.Guard();

            return httpTransactionService.BeginAsync(
                builder =>
                {
                    builder.SetServerAddress(monitorServerSetting.ServerAddress)
                           .SetLanguageCode(MonitorApiUser.LanguageCode)
                           .SetCompanyNumber(MonitorApiUser.CompanyNumber)
                           .SetUsername(MonitorApiUser.Username)
                           .SetPassword(MonitorApiUser.Password);

                    if (setCertificate)
                    {
                        if (!File.Exists(monitorServerSetting.Certificate))
                        {
                            throw new FileNotFoundException($"File not found: {monitorServerSetting.Certificate}");
                        }

                        builder.SetCertificateFile(monitorServerSetting.Certificate);
                    }

                    return builder;
                });
        }
    }
}

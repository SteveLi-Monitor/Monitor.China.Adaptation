using Domain.Common;
using Domain.Extensions;
using Monitor.API.Client;
using System.IO;
using System.Threading.Tasks;

namespace Monitor.China.Api.Bootstrap
{
    public class ApiTransaction
    {
        private readonly IHttpTransactionService httpTransactionService;

        public ApiTransaction(IHttpTransactionService httpTransactionService)
        {
            this.httpTransactionService = httpTransactionService;
        }

        public MonitorApiSetting MonitorApiSetting { get; set; }

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
            MonitorApiSetting.Guard(nameof(MonitorApiSetting));
            MonitorApiSetting.Guard();

            return httpTransactionService.BeginAsync(
                builder =>
                {
                    builder.SetServerAddress(MonitorApiSetting.MonitorServerSetting.ServerAddress)
                           .SetLanguageCode(MonitorApiSetting.MonitorApiUser.Username)
                           .SetCompanyNumber(MonitorApiSetting.MonitorApiUser.Password)
                           .SetUsername(MonitorApiSetting.MonitorApiUser.CompanyNumber)
                           .SetPassword(MonitorApiSetting.MonitorApiUser.LanguageCode);

                    if (setCertificate)
                    {
                        if (!File.Exists(MonitorApiSetting.MonitorServerSetting.Certificate))
                        {
                            throw new FileNotFoundException($"File not found: {MonitorApiSetting.MonitorServerSetting.Certificate}");
                        }

                        builder.SetCertificateFile(MonitorApiSetting.MonitorServerSetting.Certificate);
                    }

                    return builder;
                });
        }
    }
}

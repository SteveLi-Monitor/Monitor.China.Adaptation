using Domain.Common;
using Monitor.API.Client;
using System;
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

        public ApiSetting ApiSetting { get; set; }

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
            if (ApiSetting == null)
            {
                throw new ArgumentNullException(nameof(ApiSetting));
            }

            return httpTransactionService.BeginAsync(
                builder =>
                {
                    builder.SetServerAddress(ApiSetting.ServerAddress)
                           .SetLanguageCode(ApiSetting.LanguageCode)
                           .SetCompanyNumber(ApiSetting.CompanyNumber)
                           .SetUsername(ApiSetting.UserName)
                           .SetPassword(ApiSetting.Password);

                    if (setCertificate)
                    {
                        if (!File.Exists(ApiSetting.Certificate))
                        {
                            throw new FileNotFoundException($"File not found: {ApiSetting.Certificate}");
                        }

                        builder.SetCertificateFile(ApiSetting.Certificate);
                    }

                    return builder;
                });
        }
    }
}

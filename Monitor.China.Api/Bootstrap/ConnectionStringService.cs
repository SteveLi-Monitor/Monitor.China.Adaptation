using Monitor.China.Api.MonitorApis.Commands;
using Monitor.China.Api.MonitorApis.Commands.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor.China.Api.Bootstrap
{
    public class ConnectionStringService
    {
        private readonly ConfigurationCommand configurationCommand;
        private readonly ApiTransaction apiTransaction;

        public ConnectionStringService(CommandFactory commandFactory, ApiTransaction apiTransaction)
        {
            configurationCommand = commandFactory.CreateConfigurationCommand();
            this.apiTransaction = apiTransaction;
        }

        public async Task<string> GetAsync()
        {
            var configInfo = await configurationCommand.GetMonitorConfigurationAsync();
            if (configInfo == null)
            {
                throw new InvalidOperationException("Failed to find config info from API.");
            }

            var companyNumber = apiTransaction.MonitorApiUser.CompanyNumber.Substring(
                0,
                apiTransaction.MonitorApiUser.CompanyNumber.IndexOf('.'));
            var db = configInfo.Databases.FirstOrDefault(x => x.Number == companyNumber);

            if (string.IsNullOrEmpty(db?.ConnectionString))
            {
                throw new InvalidOperationException($"Failed to find connection string of database '{companyNumber}'.");
            }

            return db.ConnectionString;
        }
    }
}

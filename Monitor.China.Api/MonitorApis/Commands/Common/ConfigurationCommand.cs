using Monitor.API.Client.Repositories;
using Monitor.API.Common.Commands.Configuration;
using Monitor.China.Api.Bootstrap;
using System.Threading.Tasks;

namespace Monitor.China.Api.MonitorApis.Commands.Common
{
    public class ConfigurationCommand : CommandBase
    {
        public ConfigurationCommand(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
            : base(apiTransaction, repositoryFactory)
        {
        }

        public async Task<MonitorConfigurationInfo> GetMonitorConfigurationAsync()
        {
            var transaction = await ApiTransaction.CreateWithCertificateAsync();
            var repo = RepositoryFactory.CreateCommandRepository<GetMonitorConfiguration>(transaction);
            return await repo.Send<MonitorConfigurationInfo>(new GetMonitorConfiguration());
        }
    }
}

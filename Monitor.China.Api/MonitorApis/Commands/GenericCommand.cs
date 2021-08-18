using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;

namespace Monitor.China.Api.MonitorApis.Commands
{
    public class GenericCommand : CommandBase
    {
        public GenericCommand(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
            : base(apiTransaction, repositoryFactory)
        {
        }
    }
}

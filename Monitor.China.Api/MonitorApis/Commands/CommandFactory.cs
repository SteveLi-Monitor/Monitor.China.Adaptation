using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;

namespace Monitor.China.Api.MonitorApis.Commands
{
    public class CommandFactory
    {
        private readonly ApiTransaction apiTransaction;
        private readonly IMonitorApiRepositoryFactory monitorApiRepositoryFactory;

        public CommandFactory(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory monitorApiRepositoryFactory)
        {
            this.apiTransaction = apiTransaction;
            this.monitorApiRepositoryFactory = monitorApiRepositoryFactory;
        }

        public GenericCommand Create()
        {
            return new GenericCommand(apiTransaction, monitorApiRepositoryFactory);
        }
    }
}

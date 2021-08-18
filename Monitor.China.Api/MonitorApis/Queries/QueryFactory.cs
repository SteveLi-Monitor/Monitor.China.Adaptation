using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;

namespace Monitor.China.Api.MonitorApis.Queries
{
    public class QueryFactory
    {
        private readonly ApiTransaction apiTransaction;
        private readonly IMonitorApiRepositoryFactory monitorApiRepositoryFactory;

        public QueryFactory(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory monitorApiRepositoryFactory)
        {
            this.apiTransaction = apiTransaction;
            this.monitorApiRepositoryFactory = monitorApiRepositoryFactory;
        }

        public GenericQuery<T> Create<T>() where T : class
        {
            return new GenericQuery<T>(apiTransaction, monitorApiRepositoryFactory);
        }
    }
}

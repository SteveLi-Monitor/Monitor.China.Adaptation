using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;

namespace Monitor.China.Api.MonitorApis.Queries
{
    public class GenericQuery<T> : QueryBase<T> where T : class
    {
        public GenericQuery(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
            : base(apiTransaction, repositoryFactory)
        {
        }
    }
}

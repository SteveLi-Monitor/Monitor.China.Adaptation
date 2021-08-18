using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.China.Api.MonitorApis.Queries
{
    public abstract class QueryBase<T> where T : class
    {
        protected QueryBase(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
        {
            ApiTransaction = apiTransaction;
            RepositoryFactory = repositoryFactory;
        }

        protected ApiTransaction ApiTransaction { get; private set; }

        protected IMonitorApiRepositoryFactory RepositoryFactory { get; private set; }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateEntityRepository<T>(transaction);
            return await repo.GetAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(string builtExpression)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateEntityRepository<T>(transaction);
            return await repo.GetAsync(builtExpression);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateEntityRepository<T>(transaction);
            return await repo.GetAsync(id);
        }
    }
}

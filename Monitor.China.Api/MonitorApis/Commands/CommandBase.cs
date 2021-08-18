using Monitor.API.Client.Repositories;
using Monitor.API.Infrastructure;
using Monitor.China.Api.Bootstrap;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor.China.Api.MonitorApis.Commands
{
    public abstract class CommandBase
    {
        protected CommandBase(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
        {
            ApiTransaction = apiTransaction;
            RepositoryFactory = repositoryFactory;
        }

        protected ApiTransaction ApiTransaction { get; private set; }

        protected IMonitorApiRepositoryFactory RepositoryFactory { get; private set; }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateCommandRepository<TCommand>(transaction);
            return await repo.Send<TResult>(command);
        }

        public async Task<EntityCommandResponse> SendAsync<TCommand>(TCommand command)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateCommandRepository<TCommand>(transaction);
            return await repo.Send<EntityCommandResponse>(command);
        }

        public async Task<IEnumerable<TResult>> SendManyAsync<TCommand, TResult>(IEnumerable<TCommand> commands)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateCommandRepository<TCommand>(transaction);
            return await repo.SendMany<TResult>(commands.ToArray());
        }

        public async Task<IEnumerable<EntityCommandResponse>> SendManyAsync<TCommand>(IEnumerable<TCommand> commands)
        {
            var transaction = await ApiTransaction.CreateAsync();
            var repo = RepositoryFactory.CreateCommandRepository<TCommand>(transaction);
            return await repo.SendMany<EntityCommandResponse>(commands.ToArray());
        }
    }
}

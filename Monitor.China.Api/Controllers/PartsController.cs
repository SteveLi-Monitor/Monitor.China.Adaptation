using Microsoft.AspNetCore.Mvc;
using Monitor.API.Client.Repositories;
using Monitor.China.Api.Bootstrap;
using Monitor.China.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Inventory/Parts")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly ApiTransaction apiTransaction;
        private readonly IMonitorApiRepositoryFactory repositoryFactory;

        public PartsController(ApiTransaction apiTransaction, IMonitorApiRepositoryFactory repositoryFactory)
        {
            this.apiTransaction = apiTransaction;
            this.repositoryFactory = repositoryFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<PartDto>> Get()
        {
            var transaction = await apiTransaction.CreateAsync();
            var repo = repositoryFactory.CreateEntityRepository<PartDto>(transaction);
            return await repo.GetAsync();
        }
    }
}

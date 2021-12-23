using Dapper;
using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
using System.Data;
using System.Threading.Tasks;
using static Domain.Dtos.AutoCompletesDto;

namespace Monitor.China.Api.Controllers
{
    [Route("api/AutoCompletes")]
    [ApiController]
    public class AutoCompletesController : ControllerBase
    {
        private readonly ApiTransaction apiTransaction;
        private readonly IDbConnection dbConnection;

        public AutoCompletesController(ApiTransaction apiTransaction, SaDbConnection saDbConnection)
        {
            this.apiTransaction = apiTransaction;
            dbConnection = saDbConnection.DbConnection;
        }

        [HttpPost("Customer")]
        public async Task<ActionResult<CustomerResp>> Customer(ReqBase reqBase)
        {
            reqBase.Guard();

            var sql = @"
select
    top :PageSize
    Customer.Id as CustomerId,
    Customer.Alias as CustomerAlias,
    CustomerRoot.Name as CustomerRootName
from
    monitor.Customer
    inner join monitor.CustomerRoot on CustomerRoot.Id = Customer.RootId
where
    (Customer.Alias like :Param1 or CustomerRoot.Name like :Param1)
    and (Customer.Alias like :Param2 or CustomerRoot.Name like :Param2)
    and (Customer.Alias like :Param3 or CustomerRoot.Name like :Param3)
    and (Customer.Alias like :Param4 or CustomerRoot.Name like :Param4)
    and (Customer.Alias like :Param5 or CustomerRoot.Name like :Param5)
order by
    Customer.Alias
";

            return Ok(
                new CustomerResp
                {
                    Customers = await dbConnection.QueryAsync<CustomerResp.Customer>(sql, reqBase)
                });
        }

        [HttpPost("Part")]
        public async Task<ActionResult<PartResp>> Part(PartReq partReq)
        {
            partReq.Guard();
            partReq.LanguageId = (int)apiTransaction.MonitorApiUser.LanguageCode;

            var sql = @"
select
    top :PageSize
    Part.Id as PartId,
    Part.PartNumber as PartPartNumber,
    Part.Type as PartType,
    ExtensionsUser.Func_GetPartTranslationByPartIdAndLanguageId(Part.Id, :LanguageId) as PartDescription
from
    monitor.Part
where
    (Part.PartNumber like :Param1 or PartDescription like :Param1)
    and (Part.PartNumber like :Param2 or PartDescription like :Param2)
    and (Part.PartNumber like :Param3 or PartDescription like :Param3)
    and (Part.PartNumber like :Param4 or PartDescription like :Param4)
    and (Part.PartNumber like :Param5 or PartDescription like :Param5)
order by
    Part.PartNumber
";

            return Ok(
                new PartResp
                {
                    Parts = await dbConnection.QueryAsync<PartResp.Part>(sql, partReq)
                });
        }
    }
}

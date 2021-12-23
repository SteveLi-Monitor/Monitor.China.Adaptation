using Dapper;
using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
using System.Collections.Generic;
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

        [HttpPost("Part")]
        public async Task<ActionResult<IEnumerable<PartResp>>> Part(PartReq partReq)
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

            return Ok(await dbConnection.QueryAsync<PartResp>(sql, partReq));
        }
    }
}

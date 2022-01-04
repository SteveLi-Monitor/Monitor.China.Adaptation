using Dapper;
using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
using System.Data;
using System.Threading.Tasks;
using static Domain.Dtos.ApplicationUsersDto;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Common/ApplicationUsers")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IDbConnection dbConnection;

        public ApplicationUsersController(SaDbConnection saDbConnection)
        {
            dbConnection = saDbConnection.DbConnection;
        }

        [HttpPost("QueryApplicationUsers")]
        public async Task<ActionResult<QueryApplicationUsersResp>> QueryApplicationUsers(QueryApplicationUsersReq request)
        {
            request.Guard();

            var sql = @"
select
    ApplicationUser.Id as ApplicationUserId,
    ApplicationUser.Username as ApplicationUserUsername
from
    monitor.ApplicationUser
    inner join monitor.Person on Person.ApplicationUserId = ApplicationUser.Id
    inner join ExtensionsUser.Proc_GetPersonExtraFieldStringByIdentifier(_Identifier = :Identifier) 
        as PersonApiUserName on PersonApiUserName.PersonId = Person.Id
";

            return Ok(
                new QueryApplicationUsersResp
                {
                    Users = await dbConnection.QueryAsync<QueryApplicationUsersResp.ApplicationUser>(sql, request),
                });
        }
    }
}

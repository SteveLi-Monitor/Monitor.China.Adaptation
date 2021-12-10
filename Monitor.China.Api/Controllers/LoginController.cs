using Dapper;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Monitor.China.Api.Bootstrap;
using System.Data;
using System.Threading.Tasks;

namespace Monitor.China.Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApiTransaction apiTransaction;
        private readonly IDbConnection dbConnection;

        public LoginController(ApiTransaction apiTransaction, SaDbConnection saDbConnection)
        {
            this.apiTransaction = apiTransaction;
            dbConnection = saDbConnection.DbConnection;
        }

        [HttpPost]
        public async Task<ActionResult<LoginDto.LoginResp>> Login(LoginDto.LoginReq request)
        {
            request.Guard();

            var sql = @"
select
    ApplicationUser.Id as ApplicationUserId,
    ApplicationUser.Username as ApplicationUserUsername,
    LanguageCode.Code as LanguageCodeCode,
    Person.Id as PersonId,
    Person.EmployeeNumber as PersonEmployeeNumber,
    Person.FirstName as PersonFirstName,
    Person.LastName as PersonLastName,
    coalesce(PersonApiUserName.ExtraFieldString, ApplicationUser.Username) AS ApiUserName
from
    monitor.ApplicationUser
    inner join monitor.LanguageCode on LanguageCode.Id = ApplicationUser.LanguageId
    left outer join monitor.Person on Person.ApplicationUserId = ApplicationUser.Id
    left outer join ExtensionsUser.Proc_GetPersonExtraFieldStringByIdentifier(_Identifier = :Identifier) 
        as PersonApiUserName on PersonApiUserName.PersonId = Person.Id
where
    ApplicationUser.Username = :Username
";

            return Ok(await dbConnection.QuerySingleOrDefaultAsync<LoginDto.LoginResp>(
                sql,
                new
                {
                    Identifier = request.Identifier,
                    Username = apiTransaction.MonitorApiUser.ApiUsername,
                }));
        }
    }
}

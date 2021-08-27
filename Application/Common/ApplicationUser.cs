using Domain.Common;
using Domain.Extensions;

namespace Application.Common
{
    public class ApplicationUser : MonitorApiUser
    {
        public void CopyFrom(MonitorApiUser applicationUser)
        {
            applicationUser.Guard(nameof(applicationUser));
            applicationUser.Guard();

            Username = applicationUser.Username;
            Password = applicationUser.Password;
            LanguageCode = applicationUser.LanguageCode;
            CompanyNumber = applicationUser.CompanyNumber;
        }
    }
}

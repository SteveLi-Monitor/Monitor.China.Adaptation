using Domain.Enums;
using Domain.Extensions;

namespace Domain.Common
{
    public class MonitorApiUser
    {
        public string ApiUsername { get; set; }

        public string Password { get; set; }

        public LanguageCode LanguageCode { get; set; } = LanguageCode.ZH;

        public string CompanyNumber { get; set; }

        public void Guard()
        {
            ApiUsername.Guard(nameof(ApiUsername));
            Password.Guard(nameof(Password));
            CompanyNumber.Guard(nameof(CompanyNumber));
        }
    }
}

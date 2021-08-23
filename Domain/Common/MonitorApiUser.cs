using Domain.Extensions;

namespace Domain.Common
{
    public class MonitorApiUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string LanguageCode { get; set; } = "ZH";

        public string CompanyNumber { get; set; }

        public void Guard()
        {
            Username.Guard(nameof(Username));
            Password.Guard(nameof(Password));
            CompanyNumber.Guard(nameof(CompanyNumber));
        }
    }
}

using Domain.Extensions;

namespace Domain.Common
{
    public class ApiSetting
    {
        public string ServerAddress { get; set; }

        public string LanguageCode { get; set; }

        public string CompanyNumber { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Certificate { get; set; }

        public void Guard()
        {
            ServerAddress.Guard(nameof(ServerAddress));
            LanguageCode.Guard(nameof(LanguageCode));
            CompanyNumber.Guard(nameof(CompanyNumber));
            UserName.Guard(nameof(UserName));
            Password.Guard(nameof(Password));
        }
    }
}

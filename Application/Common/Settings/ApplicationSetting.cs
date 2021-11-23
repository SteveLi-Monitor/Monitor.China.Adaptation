using Domain.Common;
using Domain.Extensions;

namespace Application.Common.Settings
{
    public class ApplicationSetting
    {
        public MonitorApiUser DefaultApiUser { get; set; }

        public ExtraFieldIdentifiers ExtraFieldIdentifiers { get; set; }

        public void Guard()
        {
            DefaultApiUser.Guard(nameof(DefaultApiUser));
            DefaultApiUser.Guard();

            ExtraFieldIdentifiers.Guard(nameof(ExtraFieldIdentifiers));
            ExtraFieldIdentifiers.Guard();
        }
    }

    public class ExtraFieldIdentifiers
    {
        public string PersonApiUserName { get; set; }

        public void Guard()
        {
            PersonApiUserName.Guard(nameof(PersonApiUserName));
        }
    }
}

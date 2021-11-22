using Domain.Extensions;

namespace Application.Common.Settings
{
    public class ApplicationSetting
    {
        public ExtraFieldIdentifiers ExtraFieldIdentifiers { get; set; }

        public void Guard()
        {
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

using Domain.Extensions;

namespace Domain.Common
{
    public class MonitorApiSetting
    {
        public MonitorServerSetting MonitorServerSetting { get; set; }

        public MonitorApiUser MonitorApiUser { get; set; }

        public void Guard()
        {
            MonitorServerSetting.Guard(nameof(MonitorServerSetting));
            MonitorApiUser.Guard(nameof(MonitorApiUser));
            MonitorServerSetting.Guard();
            MonitorApiUser.Guard();
        }
    }
}

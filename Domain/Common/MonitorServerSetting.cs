using Domain.Extensions;

namespace Domain.Common
{
    public class MonitorServerSetting
    {
        public string ServerAddress { get; set; }

        public string Certificate { get; set; }

        public void Guard()
        {
            ServerAddress.Guard(nameof(ServerAddress));
        }
    }
}

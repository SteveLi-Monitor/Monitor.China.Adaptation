using Domain.Extensions;

namespace Application.MonitorApis
{
    public class MonitorApiServiceSetting
    {
        public string ServiceAddress { get; set; }

        public void Guard()
        {
            ServiceAddress.Guard(nameof(ServiceAddress));
        }
    }
}

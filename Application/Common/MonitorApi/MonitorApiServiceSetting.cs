using Domain.Extensions;

namespace Application.Common.MonitorApi
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

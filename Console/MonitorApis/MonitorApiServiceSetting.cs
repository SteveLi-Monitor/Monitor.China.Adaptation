using System;

namespace Console.MonitorApis
{
    public class MonitorApiServiceSetting
    {
        public string ServiceAddress { get; set; }

        public void Guard()
        {
            try
            {
                new Uri(ServiceAddress, UriKind.Absolute);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Invalid {nameof(ServiceAddress)}: {ServiceAddress}.", e);
            }
        }
    }
}

using System;

namespace Application.MonitorApis
{
    public class MonitorApiServiceSetting
    {
        public string ServiceAddress { get; set; }

        public int Timeout { get; set; } = 5;

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

            if (Timeout <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Timeout),
                    Timeout,
                    $"{nameof(Timeout)} must be greater than 0");
            }
        }
    }
}

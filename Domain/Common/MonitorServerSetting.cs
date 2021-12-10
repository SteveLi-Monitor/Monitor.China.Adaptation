using System;
using System.IO;

namespace Domain.Common
{
    public class MonitorServerSetting
    {
        public string ServerAddress { get; set; }

        public string Certificate { get; set; }

        public void Guard()
        {
            GuardServerAddress();
            GuardCertificate();
        }

        private void GuardServerAddress()
        {
            try
            {
                new Uri(ServerAddress, UriKind.Absolute);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Invalid {nameof(ServerAddress)}: {ServerAddress}.", e);
            }
        }

        private void GuardCertificate()
        {
            if (!string.IsNullOrEmpty(Certificate) && !File.Exists(Certificate))
            {
                throw new FileNotFoundException($"File not found: {Certificate}");
            }
        }
    }
}

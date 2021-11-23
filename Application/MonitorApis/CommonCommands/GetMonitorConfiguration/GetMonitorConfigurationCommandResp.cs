using System.Collections.Generic;

namespace Application.MonitorApis.CommonCommands.GetMonitorConfiguration
{
    public class GetMonitorConfigurationCommandResp
    {
        public GetMonitorConfigurationCommandResp()
        {
            Companies = new List<Company>();
        }

        public IEnumerable<Company> Companies { get; set; }

        public class Company
        {
            public string CompanyNumber { get; set; }

            public string Name { get; set; }
        }
    }
}

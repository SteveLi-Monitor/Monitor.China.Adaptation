using System.Collections.Generic;

namespace Domain.Dtos
{
    public class CommonCommandsDto
    {
        public class GetMonitorConfigurationResp
        {
            public GetMonitorConfigurationResp()
            {
                Databases = new List<Database>();
            }

            public IEnumerable<Database> Databases { get; set; }

            public class Database
            {
                public Database()
                {
                    Companies = new List<Company>();
                }

                public string Number { get; set; }

                public IEnumerable<Company> Companies { get; set; }
            }

            public class Company
            {
                public long Identifier { get; set; }

                public string Name { get; set; }
            }
        }
    }
}

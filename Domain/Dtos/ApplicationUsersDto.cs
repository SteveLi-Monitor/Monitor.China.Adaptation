using Domain.Extensions;
using System.Collections.Generic;

namespace Domain.Dtos
{
    public class ApplicationUsersDto
    {
        public class QueryApplicationUsersReq
        {
            public string Identifier { get; set; }

            public void Guard()
            {
                Identifier.Guard(nameof(Identifier));
            }
        }

        public class QueryApplicationUsersResp
        {
            public IEnumerable<ApplicationUser> Users { get; set; }

            public class ApplicationUser
            {
                public string ApplicationUserId { get; set; }

                public string ApplicationUserUsername { get; set; }
            }
        }
    }
}

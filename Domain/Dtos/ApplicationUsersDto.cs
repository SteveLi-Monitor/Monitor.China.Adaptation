using Domain.Extensions;
using System.Collections.Generic;

namespace Domain.Dtos
{
    public class ApplicationUsersDto
    {
        public class QueryWebClientUsersReq
        {
            public string Identifier { get; set; }

            public void Guard()
            {
                Identifier.Guard(nameof(Identifier));
            }
        }

        public class QueryWebClientUsersResp
        {
            public IEnumerable<WebClientUser> Users { get; set; }

            public class WebClientUser
            {
                public string ApplicationUserId { get; set; }

                public string ApplicationUserUsername { get; set; }
            }
        }
    }
}

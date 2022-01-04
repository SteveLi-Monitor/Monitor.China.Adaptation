using System.Collections.Generic;

namespace Application.Users.Queries.GetAll
{
    public class GetAllQueryResp
    {
        public IEnumerable<User> Users { get; set; }

        public class User
        {
            public string Id { get; set; }

            public string Username { get; set; }
        }
    }
}

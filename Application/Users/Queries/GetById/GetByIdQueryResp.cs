using Application.Common.Mappings;
using Application.Entities;
using System.Collections.Generic;

namespace Application.Users.Queries.GetById
{
    public class GetByIdQueryResp
    {
        public ApplicationUser User { get; set; }

        public class ApplicationUser : MapFromBase<Common.ApplicationUser>
        {
            public ApplicationUser()
            {
                UiComponents = new List<UiComponent>();
            }

            public string Id { get; set; }

            public string Username { get; set; }

            public IList<UiComponent> UiComponents { get; set; }

            public UserRole UserRole { get; set; }
        }

        public class UserRole : MapFromBase<Entities.UserRole>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public IList<UiComponent> UiComponents { get; set; }
        }
    }
}

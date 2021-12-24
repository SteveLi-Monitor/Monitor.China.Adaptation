using Application.Common.Mappings;
using Application.Entities;
using System.Collections.Generic;

namespace Application.UserRoles.Queries.GetAll
{
    public class GetAllQueryResp
    {
        public IEnumerable<UserRole> UserRoles { get; set; }

        public class UserRole : MapFromBase<Entities.UserRole>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public IList<UiComponent> AllowedUiComponents { get; set; }
        }
    }
}

using System.Collections.Generic;

namespace Application.Entities
{
    public class UserRole
    {
        public UserRole()
        {
            AllowedUiComponents = new List<UiComponent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<UiComponent> AllowedUiComponents { get; set; }
    }
}

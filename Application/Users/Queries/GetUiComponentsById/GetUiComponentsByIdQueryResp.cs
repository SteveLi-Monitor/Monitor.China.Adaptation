using Application.Entities;
using System.Collections.Generic;

namespace Application.Users.Queries.GetUiComponentsById
{
    public class GetUiComponentsByIdQueryResp
    {
        public GetUiComponentsByIdQueryResp()
        {
            UiComponents = new List<UiComponent>();
        }

        public IList<UiComponent> UiComponents { get; set; }
    }
}

using Monitor.API.Client.Attributes;
using Monitor.API.Infrastructure;

namespace Monitor.China.Api.Dtos
{
    [ReadableEntity(ApiCategory.Inventory, ApiEntityNamespace.Part)]
    public class PartDto : Domain.Dtos.PartsDto.Part
    {
        //Monitor.API.Inventory.Part
    }
}

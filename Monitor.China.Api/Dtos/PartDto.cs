using Monitor.API.Client.Attributes;
using Monitor.API.Infrastructure;

namespace Monitor.China.Api.Dtos
{
    [ReadableEntity(ApiCategory.Inventory, ApiEntityNamespace.Part)]
    public class PartDto
    {
        //Monitor.API.Inventory.Part

        public long Id { get; set; }

        public string PartNumber { get; set; }
    }
}

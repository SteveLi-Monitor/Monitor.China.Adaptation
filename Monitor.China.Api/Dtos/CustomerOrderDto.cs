using Monitor.API.Client.Attributes;
using Monitor.API.Infrastructure;

namespace Monitor.China.Api.Dtos
{
    [ReadableEntity(ApiCategory.Sales, ApiEntityNamespace.CustomerOrder)]
    public class CustomerOrderDto : Domain.Dtos.CustomerOrdersDto.CustomerOrder
    {
        //Monitor.API.Sales.CustomerOrder
    }
}

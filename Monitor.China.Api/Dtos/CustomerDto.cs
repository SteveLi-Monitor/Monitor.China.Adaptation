using Monitor.API.Client.Attributes;
using Monitor.API.Infrastructure;

namespace Monitor.China.Api.Dtos
{
    [ReadableEntity(ApiCategory.Sales, ApiEntityNamespace.Customer)]
    public class CustomerDto : Domain.Dtos.CustomersDto.Customer
    {
        //Monitor.API.Sales.Customer
    }
}

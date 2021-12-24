using Application.MonitorApis;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Dtos.AutoCompletesDto;

namespace Application.AutoCompletes.Commands.Customer
{
    public class CustomerCommand : IAutoCompleteCommand<CustomerCommandResp>
    {
        public int PageSize { get; set; } = 25;

        public string Filter { get; set; }

        public bool NeedValidation { get; set; } = true;
    }

    public class CustomerCommandHandler : IRequestHandler<CustomerCommand, CustomerCommandResp>
    {
        private readonly MonitorApiService monitorApiService;

        public CustomerCommandHandler(MonitorApiService monitorApiService)
        {
            this.monitorApiService = monitorApiService;
        }

        public async Task<CustomerCommandResp> Handle(CustomerCommand request, CancellationToken cancellationToken)
        {
            var customerResp = await monitorApiService.AutoCompleteOfCustomer(new ReqBase(request.Filter));

            return new CustomerCommandResp
            {
                Customers = customerResp.Customers.Select(x =>
                    new CustomerCommandResp.Customer
                    {
                        Id = x.CustomerId,
                        Code = x.CustomerAlias,
                        Name = x.CustomerRootName,
                    }),
            };
        }
    }
}

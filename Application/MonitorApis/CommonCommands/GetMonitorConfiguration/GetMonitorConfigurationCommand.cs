using Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MonitorApis.CommonCommands.GetMonitorConfiguration
{
    public class GetMonitorConfigurationCommand : IValidateRequest<GetMonitorConfigurationCommandResp>
    {
        public bool NeedValidation { get; set; } = true;
    }

    public class GetMonitorConfigurationCommandHandler : IRequestHandler<GetMonitorConfigurationCommand, GetMonitorConfigurationCommandResp>
    {
        private readonly MonitorApiService monitorApiService;

        public GetMonitorConfigurationCommandHandler(MonitorApiService monitorApiService)
        {
            this.monitorApiService = monitorApiService;
        }

        public async Task<GetMonitorConfigurationCommandResp> Handle(GetMonitorConfigurationCommand request, CancellationToken cancellationToken)
        {
            var getMonitorConfigurationResp = await monitorApiService.GetMonitorConfiguration();

            return new GetMonitorConfigurationCommandResp
            {
                Companies = getMonitorConfigurationResp.Databases.Select(x =>
                {
                    return new GetMonitorConfigurationCommandResp.Company
                    {
                        CompanyNumber = $"{x.Number}.{x.Companies.First().Identifier}",
                        Name = x.Companies.First().Name,
                    };
                }),
            };
        }
    }
}

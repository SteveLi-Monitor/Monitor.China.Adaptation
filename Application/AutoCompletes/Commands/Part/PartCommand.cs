using Application.MonitorApis;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Dtos.AutoCompletesDto;

namespace Application.AutoCompletes.Commands.Part
{
    public class PartCommand : CommandBase<PartCommandResp>
    {
    }

    public class PartCommandHandler : IRequestHandler<PartCommand, PartCommandResp>
    {
        private readonly MonitorApiService monitorApiService;

        public PartCommandHandler(MonitorApiService monitorApiService)
        {
            this.monitorApiService = monitorApiService;
        }

        public async Task<PartCommandResp> Handle(PartCommand request, CancellationToken cancellationToken)
        {
            var partResp = await monitorApiService.AutoCompleteOfPart(new PartReq(request.Filter));

            return new PartCommandResp
            {
                Parts = partResp.Parts.Select(x =>
                    new PartCommandResp.Part
                    {
                        Id = x.PartId,
                        PartNumber = x.PartPartNumber,
                        Type = x.PartType,
                        Description = x.PartDescription,
                    }),
            };
        }
    }
}

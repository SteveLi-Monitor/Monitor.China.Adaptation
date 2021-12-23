using Application.Common;
using Application.MonitorApis;
using Domain.Enums;
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
        private readonly ApplicationUser applicationUser;

        public PartCommandHandler(MonitorApiService monitorApiService, ApplicationUser applicationUser)
        {
            this.monitorApiService = monitorApiService;
            this.applicationUser = applicationUser;
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
                        Type = PartTypeDescription.GetDescription(applicationUser.LanguageCode, (PartType)x.PartType),
                        Description = x.PartDescription,
                    }),
            };
        }
    }
}

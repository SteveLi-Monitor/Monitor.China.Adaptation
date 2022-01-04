using Application.Common.Interfaces;
using Application.MonitorApis;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAll
{
    public class GetAllQuery : IValidateRequest<GetAllQueryResp>
    {
        public bool NeedValidation { get; set; } = true;
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllQueryResp>
    {
        private readonly MonitorApiService monitorApiService;

        public GetAllQueryHandler(MonitorApiService monitorApiService)
        {
            this.monitorApiService = monitorApiService;
        }

        public async Task<GetAllQueryResp> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var resp = await monitorApiService.QueryWebClientUsers();

            return new GetAllQueryResp
            {
                Users = resp.Users.Select(x =>
                    new GetAllQueryResp.User
                    {
                        Id = x.ApplicationUserId,
                        Username = x.ApplicationUserUsername,
                    }),
            };
        }
    }
}

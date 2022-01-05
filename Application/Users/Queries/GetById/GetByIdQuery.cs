using Application.Common.Interfaces;
using Application.MonitorApis;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetById
{
    public class GetByIdQuery : IValidateRequest<GetByIdQueryResp>
    {
        public string Id { get; set; }

        public bool NeedValidation { get; set; } = true;
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetByIdQueryResp>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly MonitorApiService monitorApiService;
        private readonly IMapper mapper;

        public GetByIdQueryHandler(
            IApplicationDbContext dbContext,
            MonitorApiService monitorApiService,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.monitorApiService = monitorApiService;
            this.mapper = mapper;
        }

        public async Task<GetByIdQueryResp> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var applicationUser = await dbContext.ApplicationUsers
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (applicationUser == null)
            {
                var resp = await monitorApiService.QueryApplicationUsers();
                var user = resp.Users.First(x => x.ApplicationUserId == request.Id);

                return new GetByIdQueryResp
                {
                    User = new GetByIdQueryResp.ApplicationUser
                    {
                        Id = user.ApplicationUserId,
                        Username = user.ApplicationUserUsername
                    }
                };
            }
            else
            {
                return new GetByIdQueryResp
                {
                    User = mapper.Map<GetByIdQueryResp.ApplicationUser>(applicationUser),
                };
            }
        }
    }
}

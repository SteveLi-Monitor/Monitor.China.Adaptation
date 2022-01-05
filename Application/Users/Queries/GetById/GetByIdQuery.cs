using Application.Common.Interfaces;
using Application.Users.Queries.GetAll;
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
        private readonly IMapper mapper;
        private readonly ISender mediator;

        public GetByIdQueryHandler(
            IApplicationDbContext dbContext,
            IMapper mapper,
            ISender mediator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<GetByIdQueryResp> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var applicationUser = await dbContext.ApplicationUsers
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (applicationUser == null)
            {
                var resp = await mediator.Send(new GetAllQuery());
                var user = resp.Users.First(x => x.Id == request.Id);

                return new GetByIdQueryResp
                {
                    User = new GetByIdQueryResp.ApplicationUser
                    {
                        Id = user.Id,
                        Username = user.Username
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

using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static Application.UserRoles.Queries.GetAll.GetAllQueryResp;

namespace Application.UserRoles.Queries.GetAll
{
    public class GetAllQuery : IValidateRequest<GetAllQueryResp>
    {
        public bool NeedValidation { get; set; } = true;
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllQueryResp>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetAllQueryResp> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return new GetAllQueryResp
            {
                UserRoles = await dbContext.UserRoles
                    .ProjectTo<UserRole>(mapper.ConfigurationProvider)
                    .ToListAsync(),
            };
        }
    }
}

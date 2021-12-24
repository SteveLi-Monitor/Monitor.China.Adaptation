using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserRoles.Commands.Create
{
    public class CreateCommand : ValidateRequestBase<Unit>, IMapTo<UserRole>
    {
        public CreateCommand()
        {
            AllowedUiComponents = new List<UiComponent>();
        }

        public string Name { get; set; }

        public IList<UiComponent> AllowedUiComponents { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommand, UserRole>()
                .ForMember(x => x.Id, option => option.Ignore());
        }
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await dbContext.UserRoles.AddAsync(mapper.Map<UserRole>(request));
            await dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

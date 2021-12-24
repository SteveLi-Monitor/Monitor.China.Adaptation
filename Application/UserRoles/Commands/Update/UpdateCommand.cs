using Application.Common.Interfaces;
using Application.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserRoles.Commands.Update
{
    public class UpdateCommand : IValidateRequest<Unit>
    {
        public UpdateCommand()
        {
            AllowedUiComponents = new List<UiComponent>();
        }

        public int Id { get; set; }

        public IList<UiComponent> AllowedUiComponents { get; set; }


        public bool NeedValidation { get; set; } = true;
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var userRole = await dbContext.UserRoles.FindAsync(request.Id);
            userRole.AllowedUiComponents = request.AllowedUiComponents;

            await dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

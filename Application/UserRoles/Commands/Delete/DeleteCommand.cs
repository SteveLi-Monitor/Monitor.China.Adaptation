using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserRoles.Commands.Delete
{
    public class DeleteCommand : IValidateRequest<Unit>
    {
        public int Id { get; set; }


        public bool NeedValidation { get; set; } = true;
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var userRole = await dbContext.UserRoles.FindAsync(request.Id);
            dbContext.UserRoles.Remove(userRole);

            await dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

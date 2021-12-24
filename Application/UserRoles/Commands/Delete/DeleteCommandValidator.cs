using Application.Common.ErrorMessages;
using Application.Common.Interfaces;
using Application.Entities;
using FluentValidation;

namespace Application.UserRoles.Commands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator(IApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id).MustAsync(
                async (id, ct) =>
                {
                    var userRole = await dbContext.UserRoles.FindAsync(id);
                    return userRole != null;
                })
                .WithMessage(x =>
                {
                    return new EntityNotFound(
                        nameof(UserRole),
                        x.Id)
                    .ToMessage();
                });
        }
    }
}

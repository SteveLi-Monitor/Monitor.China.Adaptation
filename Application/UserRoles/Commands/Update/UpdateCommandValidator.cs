using Application.Common.ErrorMessages;
using Application.Common.Interfaces;
using Application.Entities;
using Application.UiComponents;
using FluentValidation;

namespace Application.UserRoles.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator(IApplicationDbContext dbContext)
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

            RuleForEach(x => x.UiComponents).SetValidator(new UiComponentValidator());
        }
    }
}

using Application.Common.ErrorMessages;
using Application.Common.Interfaces;
using Application.Entities;
using Application.UiComponents;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRoles.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator(IApplicationDbContext dbContext)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MustAsync(
                (name, ct) =>
                {
                    return dbContext.UserRoles.AllAsync(x => x.Name != name);
                })
                .WithMessage(x =>
                {
                    return new EntityAlreadyExists(
                        nameof(UserRole),
                        nameof(UserRole.Name),
                        x.Name)
                    .ToMessage();
                });

            RuleForEach(x => x.AllowedUiComponents).SetValidator(new UiComponentValidator());
        }
    }
}

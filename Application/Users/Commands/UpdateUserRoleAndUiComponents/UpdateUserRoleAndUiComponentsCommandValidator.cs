using Application.Common;
using Application.Common.ErrorMessages;
using Application.Common.Interfaces;
using Application.Entities;
using Application.MonitorApis;
using Application.UiComponents;
using FluentValidation;
using System.Linq;

namespace Application.Users.Commands.UpdateUserRoleAndUiComponents
{
    public class UpdateUserRoleAndUiComponentsCommandValidator : AbstractValidator<UpdateUserRoleAndUiComponentsCommand>
    {
        public UpdateUserRoleAndUiComponentsCommandValidator(
            IApplicationDbContext dbContext,
            MonitorApiService monitorApiService)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).MustAsync(
                async (id, ct) =>
                {
                    var resp = await monitorApiService.QueryApplicationUsers();
                    return resp.Users.Any(x => x.ApplicationUserId == id);
                })
                .WithMessage(x =>
                {
                    return new EntityNotFound(
                        nameof(ApplicationUser),
                        x.Id)
                    .ToMessage();
                });

            RuleFor(x => x.UserRoleId).MustAsync(
                async (id, ct) =>
                {
                    var userRole = await dbContext.UserRoles.FindAsync(id);
                    return userRole != null;
                })
                .When(x => x.UserRoleId.HasValue)
                .WithMessage(x =>
                {
                    return new EntityNotFound(
                        nameof(UserRole),
                        x.UserRoleId.Value)
                    .ToMessage();
                });

            RuleForEach(x => x.UiComponents).SetValidator(new UiComponentValidator());
        }
    }
}

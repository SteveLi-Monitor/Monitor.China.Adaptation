using Application.Common;
using Application.Common.ErrorMessages;
using Application.MonitorApis;
using FluentValidation;
using System.Linq;

namespace Application.Users.Queries.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator(MonitorApiService monitorApiService)
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
        }
    }
}

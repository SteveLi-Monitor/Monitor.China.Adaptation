using Application.Common;
using Application.Common.ErrorMessages;
using Application.Users.Queries.GetAll;
using FluentValidation;
using MediatR;
using System.Linq;

namespace Application.Users.Queries.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator(ISender mediator)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).MustAsync(
                async (id, ct) =>
                {
                    var resp = await mediator.Send(new GetAllQuery());
                    return resp.Users.Any(x => x.Id == id);
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

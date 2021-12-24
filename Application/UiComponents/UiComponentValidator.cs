using Application.Entities;
using FluentValidation;

namespace Application.UiComponents
{
    public class UiComponentValidator : AbstractValidator<UiComponent>
    {
        public UiComponentValidator()
        {
            RuleFor(x => x.Section).NotEmpty();
            RuleFor(x => x.Module).NotEmpty();
            RuleFor(x => x.Page).NotEmpty();
        }
    }
}

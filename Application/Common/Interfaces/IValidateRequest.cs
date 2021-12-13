using MediatR;

namespace Application.Common.Interfaces
{
    public interface IValidateRequest<out TResponse> : IRequest<TResponse>
    {
        bool NeedValidation { get; set; }
    }
}

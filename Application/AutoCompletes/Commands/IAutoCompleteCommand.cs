using Application.Common.Interfaces;

namespace Application.AutoCompletes.Commands
{
    public interface IAutoCompleteCommand<TResponse> : IValidateRequest<TResponse>
    {
        int PageSize { get; set; }

        string Filter { get; set; }
    }
}

using Application.Common.Interfaces;

namespace Application.AutoCompletes.Commands
{
    public abstract class CommandBase<TResponse> : ValidateRequestBase<TResponse>
    {
        public int PageSize { get; set; } = 25;

        public string Filter { get; set; }
    }
}

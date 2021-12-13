namespace Application.Common.Interfaces
{
    public abstract class ValidateRequestBase<TResponse> : IValidateRequest<TResponse>
    {
        public bool NeedValidation { get; set; } = true;
    }
}

using Application.Common.Exceptions;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class ExceptionLogBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).FullName;

            try
            {
                Log.Debug($"Request started: {requestName}.");
                var resp = await next();

                Log.Debug($"Request finished: {requestName}.");
                return resp;
            }
            catch (ValidationException validationEx)
            {
                Log.Error($"Validation failed for request {requestName}:" + Environment.NewLine +
                    $"Request: {JsonConvert.SerializeObject(request, Formatting.Indented)}" + Environment.NewLine +
                    $"Errors: {JsonConvert.SerializeObject(validationEx.Errors, Formatting.Indented)}");

                throw;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Unhandled exception for request {requestName}:" + Environment.NewLine +
                    $"Request: {JsonConvert.SerializeObject(request, Formatting.Indented)}");

                throw;
            }
        }
    }
}

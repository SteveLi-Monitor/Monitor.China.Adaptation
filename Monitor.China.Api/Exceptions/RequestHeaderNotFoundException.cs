using System;

namespace Monitor.China.Api.Exceptions
{
    public class RequestHeaderNotFoundException : Exception
    {
        public RequestHeaderNotFoundException(string key)
            : base($"Request header not found: {key}")
        {
        }
    }
}

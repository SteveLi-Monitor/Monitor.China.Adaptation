using System;

namespace Domain.Exceptions
{
    public class InvalidRequestHeaderException : Exception
    {
        public InvalidRequestHeaderException(string key, string value)
            : base($"Invalid request header: {key}. Header value: {value}.")
        {
        }

        public InvalidRequestHeaderException(string key, string value, Exception innerException)
            : base($"Invalid request header: {key}. Header value: {value}.", innerException)
        {
        }
    }
}

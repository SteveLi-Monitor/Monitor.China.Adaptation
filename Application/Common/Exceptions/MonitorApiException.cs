using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Common.Exceptions
{
    public class MonitorApiException : Exception
    {
        public MonitorApiException(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public ProblemDetails ProblemDetails { get; set; }
    }
}

using System;
using System.Net;

namespace Email.Application.Exceptions
{
    public class BaseApplicationException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BaseApplicationException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

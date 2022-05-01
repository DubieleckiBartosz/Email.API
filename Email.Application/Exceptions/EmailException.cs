using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Email.Application.Exceptions
{
    public class EmailException : BaseApplicationException
    {
        public EmailException(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(code, message)
        {
        }
    }
}

using System.Security.AccessControl;

namespace Email.Application.Models
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string ServiceName { get; set; }
    }
}

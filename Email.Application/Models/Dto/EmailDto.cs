using System.Collections.Generic;

namespace Email.Application.Models.Dto
{
    public class EmailDto
    {
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public string SubjectMail { get; set; }
        public string  ServiceName { get; set; }
        public Dictionary<string, string> DictionaryData { get; set; }
        public List<string> Recipients { get; set; }
    }
}

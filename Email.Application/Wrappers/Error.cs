using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Email.Application.Wrappers
{
    public class Error
    {
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}

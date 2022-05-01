using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Email.Application.Models.Dao
{
    public class Template
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TemplateType { get; set; }
        public string TemplateName { get; set; }
        public string TemplateContent { get; set; }
    }
}

using Email.Application.Interfaces.Repository;
using Email.Application.Models.Dao;
using Email.Application.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Email.Infrastructure.Database
{
    public class TemplateContext : ITemplateContext
    {
        private readonly MongoSettings _mongoSettings;

        public TemplateContext(IOptions<MongoSettings> mongoSettings)
        {
            _mongoSettings = mongoSettings.Value;
            var client = new MongoClient(_mongoSettings.MongoConnection);
            var db = client.GetDatabase(_mongoSettings.DatabaseName);
            Templates = db.GetCollection<Template>(_mongoSettings.CollectionName);
            TemplateSeed.SeedData(Templates);
        }

        public IMongoCollection<Template> Templates { get; }
    }
}

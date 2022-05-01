using Email.Application.Models.Dao;
using MongoDB.Driver;

namespace Email.Application.Interfaces.Repository
{
    public interface ITemplateContext
    {
        IMongoCollection<Template> Templates { get; }
    }
}

using System.Threading.Tasks;
using Email.Application.Models.Dao;

namespace Email.Application.Interfaces.Repository
{
    public interface ITemplateRepository
    {
        Task<Template> GetTemplate(string key, string tempName);
    }
}

using System;
using System.Threading.Tasks;
using Email.Application.Interfaces.Repository;
using Email.Application.Models.Dao;
using Email.Application.Responses;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Email.Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ITemplateContext _context;
        private readonly ICacheRepository _cacheRepository;
        private readonly ILogger<TemplateRepository> _logger;

        public TemplateRepository(ITemplateContext context, ICacheRepository cacheRepository,
            ILogger<TemplateRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
            _logger = logger;
            ;
        }

        //public async Task<Application.Models.Template> GetTemplateById(string templateId)
        //{
        //    var filter = Builders<Application.Models.Template>.Filter.Eq(e => e.CompanyId, templateId);
        //    var result = await _context.Templates.Find(filter).FirstOrDefaultAsync();
        //    return result;
        //}

        public async Task<Template> GetTemplate(string key, string tempName)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError(CacheStrings.KeyCacheIncorrect);
                return null;
            }

            if (string.IsNullOrEmpty(tempName))
            {
                //exception or sth
                return null;
            }

            var template = await _cacheRepository.GetAsync<Template>(key);

            if (template == null)
            {
                template = await _context.Templates.Find(f => f.TemplateName == tempName).FirstOrDefaultAsync();
                if (template == null)
                {
                    _logger.LogError(TempStrings.TemplateDbNotFound);
                    return null;
                }

                var resultSaveData =
                    await _cacheRepository.SetAsync<Template>(key, template,
                        TimeSpan.FromHours(24));
                if (!resultSaveData)
                {
                    _logger.LogError(CacheStrings.SaveFailed);
                }
                else
                {
                    _logger.LogInformation(CacheStrings.SavePositive(key));
                }
                return template;
            }

            return template;
        }
    }
}

using System;
using System.Threading.Tasks;
using Email.Application.Cache;
using Email.Application.Helpers;
using Email.Application.Interfaces.Repository;
using Email.Application.Interfaces.Service;
using Email.Application.Models.Dto;
using Email.Application.Responses;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Email.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailService;
        private readonly ITemplateRepository _templateService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IEmailRepository emailService, ITemplateRepository templateService,
            IBackgroundJobClient backgroundJobClient, ILogger<EmailService> logger)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmailData(EmailDto emailData)
        {
            var templateName = emailData.TemplateName;

            var key = string.Format(CacheKey.CreateCacheKey, templateName, emailData.TemplateType);
            var template = await _templateService.GetTemplate(key, templateName) ??
                           throw new ArgumentNullException(TempStrings.TemplateDoesNotExist);

            var readyTemplate = template.TemplateContent.GetTemplateReplaceData(emailData.DictionaryData);
            emailData.Recipients.ForEach(x => SendEmail(x, readyTemplate, emailData.SubjectMail));
            return true;

        }

        private void SendEmail(string to, string body, string subject)
        {
            var email = new Models.Email
            {
                To = to,
                Body = body,
                Subject = subject
            };
            _backgroundJobClient.Enqueue(() => _emailService.SendEmailAsync(email));
        }
    }
}

using System;
using System.Threading.Tasks;
using Email.Application.Exceptions;
using Email.Application.Interfaces.Repository;
using Email.Application.Responses;
using Email.Application.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Email.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ILogger<EmailRepository> _logger;
        private readonly EmailSettings _emailSettings;

        public EmailRepository(IOptions<EmailSettings> settings, ILogger<EmailRepository> logger)
        {
            _emailSettings = settings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(Application.Models.Email email)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            mailMessage.To.Add(MailboxAddress.Parse(email.To));
            mailMessage.Subject = email.Subject;
            mailMessage.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    await smtpClient.AuthenticateAsync(_emailSettings.User, _emailSettings.Password);
                    await smtpClient.SendAsync(mailMessage);
                    await smtpClient.DisconnectAsync(true);
                }

                _logger.LogInformation($"Send email to {email.To}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"From email service: {ex.Message}");
                throw new EmailException(EmailResponseStrings.EmailFailed);
            }
        }
    }
}

using System.Threading.Tasks;
using Email.Application.Models.Dto;

namespace Email.Application.Interfaces.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmailData(EmailDto emailData);
    }
}

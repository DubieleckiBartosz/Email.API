using System.Threading.Tasks;

namespace Email.Application.Interfaces.Repository
{
    public interface IEmailRepository
    {
        public Task<bool> SendEmailAsync(Models.Email emailModel);
    }
}

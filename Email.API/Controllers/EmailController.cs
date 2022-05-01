using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Email.Application.Interfaces.Service;
using Email.Application.Models.Dto;

namespace Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendMail([FromBody] EmailDto email)
        {
            return Ok(await _emailService.SendEmailData(email));
        }
    }
}

using MailTestService.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MailTestProject.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult GetEmails(string email)
        {
            var emails = _emailService.GetEmailsByAddress(email);
            return ViewComponent("Chat", new { emails = emails });
        }
    }
}

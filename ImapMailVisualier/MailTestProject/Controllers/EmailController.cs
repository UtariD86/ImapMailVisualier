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
        public IActionResult GetEmails(string email, bool fromDb)
        {
            try
            {
                var emails = fromDb ? _emailService.GetEmailsByAddressFromDb(email) : _emailService.GetEmailsByAddress(email);
                return ViewComponent("Chat", new { emails = emails });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> TransferEmails()
        {
            try
            {
                var result = await _emailService.TransferToDB();
                return Json(new { success = true, message = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}

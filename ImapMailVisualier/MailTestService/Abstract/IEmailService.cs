using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailTestService.Dtos;
using MimeKit;

namespace MailTestService.Abstract
{
    public interface IEmailService
    {
        List<MimeMessage> FetchEmails();
        List<EmailDto> GetEmailsByAddress(string address);
    }
}

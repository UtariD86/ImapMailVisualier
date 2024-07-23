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
        Task<string> TransferToDB();
        List<EmailDto> GetEmailsByAddress(string address);
        List<EmailDto> GetEmailsByAddressFromDb(string address);
    }
}

using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MailTestDataAccess.Repositories.Concrete;
using MailTestDomain.Entities;
using MailTestService.Abstract;
using MailTestService.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailTestService.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly IEmailMessageRepository _emailMessageRepository;
        private readonly IConfiguration _configuration;

        public EmailManager(IEmailMessageRepository emailMessageRepository, IConfiguration configuration)
        {
            _emailMessageRepository = emailMessageRepository;
            _configuration = configuration;
        }

        public List<MimeMessage> FetchEmails()
        {
            using (var client = new ImapClient())
            {
                try
                {
                    // Ayarları appsettings.json'dan oku
                    var email = _configuration["EmailSettings:Email"];
                    var password = _configuration["EmailSettings:Password"];
                    var imapServer = _configuration["EmailSettings:ImapServer"];
                    var imapPort = int.Parse(_configuration["EmailSettings:ImapPort"]);
                    var useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"]);

                    // Bağlantı kur
                    client.Connect(imapServer, imapPort, useSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);

                    // Kimlik Doğrulaması
                    client.Authenticate(email, password);

                    // Gelen kutusu aç
                    client.Inbox.Open(FolderAccess.ReadOnly);

                    var emails = new List<MimeMessage>();
                    for (int i = 0; i < client.Inbox.Count; i++)
                    {
                        var message = client.Inbox.GetMessage(i);
                        emails.Add(message);
                    }

                    // Gönderilmiş öğelerden e-posta alma
                    var sentFolder = client.GetFolder(SpecialFolder.Sent);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    for (int i = 0; i < sentFolder.Count; i++)
                    {
                        var message = sentFolder.GetMessage(i);
                        emails.Add(message);
                    }

                    // Bağlantıyı Kes
                    client.Disconnect(true);

                    return emails;
                }
                catch (Exception ex)
                {
                    // Hataları işle
                    throw new ApplicationException("E-posta alma işlemi sırasında hata oluştu", ex);
                }
            }
        }

        public List<EmailDto> GetEmailsByAddress(string address)
        {
            var emails = FetchEmails();
            var emailDtos = new List<EmailDto>();

            foreach (var email in emails)
            {
                var isOutgoing = email.To.Mailboxes.Any(m => m.Address == address);
                var isIncoming = email.From.Mailboxes.Any(m => m.Address == address);
                var from = email.From.Mailboxes.FirstOrDefault();

                if (isIncoming || isOutgoing)
                {
                    var emailDto = new EmailDto
                    {
                        Date = email.Date.ToString(),
                        Subject = email.Subject,
                        Body = email.TextBody ?? email.HtmlBody,
                        FromAddress = from?.Address,
                        FromName = from?.Name,
                        FromAvatar = GetAvatarUrl(from?.Address),
                        IsIncoming = isIncoming
                    };

                    emailDtos.Add(emailDto);
                }
            }

            return emailDtos;
        }

        public List<EmailDto> GetEmailsByAddressFromDb(string address)
        {
            // Tüm e-postaları alır ve filtreler
            var emails = _emailMessageRepository.GetAll()
                            .Where(x => x.OwnerEmail == _configuration["EmailSettings:Email"] &&
                                        (x.FromAddress == address || x.To == address))
                            .ToList();

            // E-postaları EmailDto nesnelerine dönüştürür
            var emailDtos = emails.Select(email =>
            {
                var isIncoming = email.FromAddress == address; // Gelen e-posta olup olmadığını kontrol ederiz
                return new EmailDto
                {
                    Date = email.Date.ToString(), // E-posta tarihini string olarak alır
                    Subject = email.Subject, // E-posta konusunu alır
                    Body = email.BodyHtml, // E-posta HTML gövdesini alır
                    FromAddress = email.FromAddress, // Gönderen adresini alır
                    FromName = email.FromName, // Gönderen adını alır
                    FromAvatar = GetAvatarUrl(email.FromAddress), // Gönderenin avatar URL'sini alır
                    IsIncoming = isIncoming // E-postanın gelen e-posta olup olmadığını belirler
                };
            }).ToList();

            return emailDtos; // EmailDto listesini döndürür
        }
        public async Task<string> TransferToDB()
        {
            try
            {
                var _email = _configuration["EmailSettings:Email"];
                var newEmails = FetchEmails();

                var oldEmails = await _emailMessageRepository.GetAll()
                    //.Where(x => x.From == _email || x.To == _email)
                    .ToListAsync();

                var oldMessageIds = oldEmails.Select(e => e.MessageId).ToHashSet();

                var newEntities = newEmails.Where(e => !oldMessageIds.Contains(e.MessageId))
                    .Select(e => new EmailMessage
                    {
                        MessageId = e.MessageId,
                        FromName = e.From.Mailboxes.FirstOrDefault()?.Name ?? "",
                        FromAddress = e.From.Mailboxes.FirstOrDefault()?.Address ?? "",
                        To = e.To.Mailboxes.FirstOrDefault()?.Address ?? "",
                        Subject = e.Subject ?? "",
                        MimeVersion = e.MimeVersion?.ToString()?? "",
                        ContentType = e.BodyParts.FirstOrDefault()?.ContentType?.ToString() ?? "",
                        XPriority = e.Headers["X-Priority"]?.ToString() ?? "",
                        XMSMailPriority = e.Headers["X-MSMail-Priority"]?.ToString() ?? "",
                        XMailer = e.Headers["X-Mailer"]?.ToString() ?? "",
                        XMimeOLE = e.Headers["X-MimeOLE"]?.ToString() ?? "",
                        Date = e.Date.DateTime,
                        XRead = e.Headers["X-Read"] != null,
                        BodyPlainText = e.BodyParts.OfType<TextPart>().FirstOrDefault(tp => tp.IsHtml == false)?.Text ?? "",
                        BodyHtml = e.BodyParts.OfType<TextPart>().FirstOrDefault(tp => tp.IsHtml == true)?.Text ?? "",
                        Attachments = e.Attachments != null ? string.Join(", ", e.Attachments.Select(a => a.ContentDisposition?.FileName ?? a.ContentType.ToString())) : "",
                        OwnerEmail = _email
                    }).ToList();

                if(newEntities.Count < 1 )
                    return "Yeni E posta bulunamadı";

                await _emailMessageRepository.BulkInsert(newEntities);

                return "E-postalar başarıyla veritabanına aktarıldı.";
            }
            catch (Exception ex)
            {
                return $"E-posta aktarımı sırasında bir hata oluştu: {ex.Message}";
            }
        }


        private string GetAvatarUrl(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return null;

            var hash = System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.ASCII.GetBytes(emailAddress.Trim().ToLower()));
            var hashString = string.Concat(hash.Select(b => b.ToString("x2")));

            return $"https://www.gravatar.com/avatar/{hashString}";
        }
    }
}

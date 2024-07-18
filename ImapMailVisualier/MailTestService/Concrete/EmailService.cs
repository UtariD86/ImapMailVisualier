using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MailTestService.Abstract;
using MailTestService.Dtos;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailTestService.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _imapServer;
        private readonly int _imapPort;
        private readonly bool _useSsl;

        public EmailService(string email, string password, string imapServer, int imapPort, bool useSsl = true)
        {
            _email = email;
            _password = password;
            _imapServer = imapServer;
            _imapPort = imapPort;
            _useSsl = useSsl;
        }

        public List<MimeMessage> FetchEmails()
        {
            using (var client = new ImapClient())
            {
                try
                {
                    // Connect to the IMAP server
                    //client.Connect(_imapServer, _imapPort, SecureSocketOptions.StartTls);
                    client.Connect(_imapServer, 993, SecureSocketOptions.SslOnConnect);

                    // Authenticate
                    client.Authenticate(_email, _password);

                    // Fetch emails
                    client.Inbox.Open(FolderAccess.ReadOnly);

                    var emails = new List<MimeMessage>();
                    for (int i = 0; i < client.Inbox.Count; i++)
                    {
                        var message = client.Inbox.GetMessage(i);
                        emails.Add(message);
                    }

                    // Fetch emails from Sent Items
                    var sentFolder = client.GetFolder(SpecialFolder.Sent);
                    sentFolder.Open(FolderAccess.ReadOnly);
                    for (int i = 0; i < sentFolder.Count; i++)
                    {
                        var message = sentFolder.GetMessage(i);
                        emails.Add(message);
                    }

                    // Disconnect
                    client.Disconnect(true);

                    return emails;
                }
                catch (SslHandshakeException ex)
                {
                    // Handle SSL/TLS handshake errors
                    throw new ApplicationException("SSL/TLS handshake error", ex);
                }
                catch (AuthenticationException ex)
                {
                    // Handle authentication errors
                    throw new ApplicationException("Authentication error", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new ApplicationException("Error fetching emails", ex);
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

        private string GetAvatarUrl(string emailAddress)
        {
            // Burada bir gravatar veya avatar URL'si döndürebilirsiniz
            // Örneğin, gravatar kullanabilirsiniz:
            if (string.IsNullOrEmpty(emailAddress))
                return null;

            var hash = System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.ASCII.GetBytes(emailAddress.Trim().ToLower()));
            var hashString = string.Concat(hash.Select(b => b.ToString("x2")));

            return $"https://www.gravatar.com/avatar/{hashString}";
        }
    }
}

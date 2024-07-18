using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTestService.Dtos
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTestService.Dtos
{
    public class EmailDto
    {
        public string Date { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string FromAvatar { get; set; }
        public bool IsIncoming { get; set; }
    }
}

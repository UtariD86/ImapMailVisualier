using MailTestDataAccess.Context;
using MailTestDataAccess.Repositories.Concrete;
using MailTestDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTestDataAccess.Repositories.Application.Concrete
{
    public class EfEmailMessageRepository : Repository<EmailMessage>, IEmailMessageRepository
    {
        private readonly mailTestContext _context;

        public EfEmailMessageRepository(mailTestContext context) : base(context)
        {
            _context = context;
        }
    }
}

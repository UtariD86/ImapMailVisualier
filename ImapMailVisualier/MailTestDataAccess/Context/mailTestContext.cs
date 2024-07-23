using MailTestDataAccess.Mappings;
using MailTestDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MailTestDataAccess.Context
{
    public class mailTestContext : DbContext
    {
        public DbSet<EmailMessage> EmailMessages { get; set; }

        public mailTestContext(DbContextOptions<mailTestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EmailMessageMap());
        }
    }
}

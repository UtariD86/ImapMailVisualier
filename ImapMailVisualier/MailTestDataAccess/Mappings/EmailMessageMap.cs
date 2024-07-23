using MailTestDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTestDataAccess.Mappings
{
    public class EmailMessageMap : IEntityTypeConfiguration<EmailMessage>
    {
        public void Configure(EntityTypeBuilder<EmailMessage> builder)
        {
            builder.ToTable("EmailMessages");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.MessageId)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.FromName)
                   .HasMaxLength(255)
                   .IsRequired();
            
            builder.Property(e => e.FromAddress)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.To)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(e => e.Subject)
                   .HasMaxLength(255);

            builder.Property(e => e.MimeVersion)
                   .HasMaxLength(100);

            builder.Property(e => e.ContentType)
                   .HasMaxLength(100);

            builder.Property(e => e.XPriority)
                   .HasMaxLength(50);

            builder.Property(e => e.XMSMailPriority)
                   .HasMaxLength(50);

            builder.Property(e => e.XMailer)
                   .HasMaxLength(255);

            builder.Property(e => e.XMimeOLE)
                   .HasMaxLength(255);

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.XRead)
                   .IsRequired();

            builder.Property(e => e.BodyPlainText)
                   .HasColumnType("text");

            builder.Property(e => e.BodyHtml)
                   .HasColumnType("text");

            builder.Property(e => e.Attachments)
                   .HasColumnType("text");

            builder.Property(e => e.OwnerEmail)
                   .HasMaxLength(255);
        }
    }
}

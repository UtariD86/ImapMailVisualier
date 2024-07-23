using MailTestDomain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTestDomain.Entities
{
    /// <summary>
    /// E-posta mesajını ve ilgili tüm özelliklerini temsil eder.
    /// </summary>
    public class EmailMessage : IEntity
    {
        /// <summary>
        /// E-posta mesajının benzersiz kimliğini alır veya ayarlar.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// E-postanın mesaj kimliğini alır veya ayarlar.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Gönderenin e-posta adresini alır veya ayarlar.
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gönderenin e-posta adresini alır veya ayarlar.
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// Alıcının e-posta adresini alır veya ayarlar.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// E-postanın konusunu alır veya ayarlar.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// E-postanın MIME sürümünü alır veya ayarlar.
        /// </summary>
        public string MimeVersion { get; set; }

        /// <summary>
        /// E-postanın içerik türünü alır veya ayarlar.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// E-postanın önceliğini alır veya ayarlar.
        /// </summary>
        public string XPriority { get; set; }

        /// <summary>
        /// E-postanın Microsoft Mail önceliğini alır veya ayarlar.
        /// </summary>
        public string XMSMailPriority { get; set; }

        /// <summary>
        /// E-postayı göndermek için kullanılan posta programını alır veya ayarlar.
        /// </summary>
        public string XMailer { get; set; }

        /// <summary>
        /// E-postayı göndermek için kullanılan MIME OLE'yi alır veya ayarlar.
        /// </summary>
        public string XMimeOLE { get; set; }

        /// <summary>
        /// E-postanın gönderildiği tarihi alır veya ayarlar.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// E-postanın okunup okunmadığını belirten değeri alır veya ayarlar.
        /// </summary>
        public bool XRead { get; set; }

        /// <summary>
        /// E-postanın düz metin gövdesini alır veya ayarlar.
        /// </summary>
        public string BodyPlainText { get; set; }

        /// <summary>
        /// E-postanın HTML gövdesini alır veya ayarlar.
        /// </summary>
        public string BodyHtml { get; set; }

        /// <summary>
        /// E-postanın eklerini, base64 kodlu olarak saklanmış stringler halinde alır veya ayarlar.
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// E-postanın sahibinin e-posta adresini alır veya ayarlar.
        /// </summary>
        public string OwnerEmail { get; set; }
    }

}

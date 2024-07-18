# ImapMailVisualier

IMAP Email Chat Interface

This project demonstrates a service that connects to an IMAP server to fetch and display email communications in a chat-like interface. It uses the MailKit library for secure connection, authentication, and email retrieval from both the Inbox and Sent Items folders.

## Key Features

- **IMAP Connection**: Securely connects to an IMAP server using SSL/TLS.
- **Authentication**: Authenticates using email and password.
- **Fetch Emails**: Retrieves emails from both the Inbox and Sent Items folders.
- **Error Handling**: Gracefully handles SSL/TLS handshake errors and authentication errors.
- **Email Parsing**: Parses and formats emails into a chat-like structure.
- **Gravatar Integration**: Retrieves avatar images for email addresses using Gravatar.

## Getting Started

To run the project locally:

1. Clone this repository.
2. Configure your email server settings in `appsettings.json`.
3. Build and run the project.

### Prerequisites

- .NET SDK
- MailKit NuGet package
- Gravatar library (optional for avatar integration)

### Usage

Configure your IMAP server details in `appsettings.json`:

```json
"EmailSettings": {
  "Email": "ornek@gmail.com",
  "Password": "orneksifre",
  "ImapServer": "imap.gmail.com",
  "ImapPort": 993
},
```

Instructions:

Replace "ornek@gmail.com" with your Gmail email address.
Replace "orneksifre" with your Gmail application-specific password.
Ensure "ImapServer" is set to "imap.gmail.com" for Gmail IMAP settings.
Set "ImapPort" to 993 for Gmail's IMAP port.
Build and run the application. It will connect to the specified IMAP server, fetch emails, and display them in a chat-like interface.

Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgments
MailKit library for IMAP functionality.
Gravatar for avatar retrieval based on email addresses.

----------------------------------------------------------------------------------------------------------------------------------------------------
# MailTestProject

IMAP E-posta Sohbet Arayüzü

Bu proje, IMAP sunucusuna bağlanarak e-posta iletişimlerini bir sohbet benzeri arayüzde almak ve görüntülemek için bir hizmet sağladığını gösterir. Güvenli bağlantı, kimlik doğrulama ve Inbox ve Sent Items klasörlerinden e-posta alımı için MailKit kütüphanesini kullanır.

## Temel Özellikler

- **IMAP Bağlantısı**: SSL/TLS kullanarak IMAP sunucusuna güvenli bağlantı sağlar.
- **Kimlik Doğrulama**: E-posta ve şifre kullanarak kimlik doğrular.
- **E-posta Alımı**: Hem Inbox hem de Sent Items klasörlerinden e-postaları alır.
- **Hata Yönetimi**: SSL/TLS el sıkışma hatalarını ve kimlik doğrulama hatalarını düzgün bir şekilde yönetir.
- **E-posta Ayrıştırma**: E-postaları bir sohbet benzeri yapıya ayrıştırır ve biçimlendirir.
- **Gravatar Entegrasyonu**: Gravatar kullanarak e-posta adresleri için avatar görüntüleri alır.

## Başlarken

Projeyi yerel olarak çalıştırmak için:

1. Bu depoyu klonlayın.
2. `appsettings.json` dosyasında e-posta sunucu ayarlarınızı yapılandırın.
3. Projeyi derleyip çalıştırın.

### Önkoşullar

- .NET SDK
- MailKit NuGet paketi
- Gravatar kütüphanesi (avatar entegrasyonu için isteğe bağlı)

### Kullanım

IMAP sunucu detaylarınızı `appsettings.json` dosyasında yapılandırın:

```json
"EmailSettings": {
  "Email": "ornek@gmail.com",
  "Password": "orneksifre",
  "ImapServer": "imap.gmail.com",
  "ImapPort": 993
},
```
Açıklamalar:

"ornek@gmail.com" yerine kendi Gmail e-posta adresinizi yazın.
"orneksifre" yerine Gmail uygulama özel şifrenizi yazın.
"ImapServer"'ı Gmail IMAP ayarları için "imap.gmail.com" olarak ayarlayın.
"ImapPort"'u Gmail'in IMAP portu olan 993 olarak ayarlayın.
Projeyi derleyip çalıştırın. Belirtilen IMAP sunucusuna bağlanacak, e-postaları alacak ve bunları bir sohbet benzeri arayüzde görüntüleyecektir.

Katkıda Bulunma
Katkılarınızı bekliyoruz! Lütfen depoyu çatallayın ve değişikliklerinizle bir pull request gönderin.

Lisans
Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için LICENSE dosyasına bakınız.

Teşekkürler
IMAP işlevselliği için MailKit kütüphanesi.
E-posta adreslerine dayalı avatar alımı için Gravatar.

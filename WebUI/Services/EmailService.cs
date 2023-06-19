using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using WebUI.Models;

namespace WebUI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendAutoResponse(string name, string recipientEmail) // Benimle iletişime geçenlere oto mail atıyorum.
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Gönderen Adı", _configuration.GetSection("EmailUsername").Value));
            emailMessage.To.Add(new MailboxAddress(name, recipientEmail));
            emailMessage.Subject = "İletişim Formu Yanıtı";
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Merhaba {name} Mesajınızı Aldık. Bu Bir Otomatik Mesajdır. Lütfen Bu Postaya Cevap Vermeyin. Size En Kısa Sürede Geri Döneceğiz. Teşekkür Ederiz!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                client.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public void SendMail(ContactDto request) 
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Gönderen Adı", request.email));
            emailMessage.To.Add(new MailboxAddress("Alıcı Adı", _configuration.GetSection("EmailUsername").Value));
            emailMessage.Subject = request.subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Ad: {request.name}\nE-posta: {request.email}\nKonu: {request.subject}\n \nMesaj: {request.message}"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailMessage);
            smtp.Disconnect(true);
        }


    }
}

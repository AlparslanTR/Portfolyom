using WebUI.Models;

namespace WebUI.Services
{
    public interface IEmailService
    {
        void SendMail(ContactDto request);
        void SendAutoResponse(string name, string recipientEmail);
    }
}

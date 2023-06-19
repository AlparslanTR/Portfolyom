using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MimeKit;
using MimeKit.Text;
using NToastNotify;
using System.Text.RegularExpressions;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class HakkımdaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IToastNotification _toastNotification;
        private LanguageService _localization;

        public HakkımdaController(IConfiguration configuration, IEmailService emailService, IToastNotification toastNotification, LanguageService localization)
        {
            _configuration = configuration;
            _emailService = emailService;
            _toastNotification = toastNotification;
            _localization = localization;
        }

        public IActionResult Hakkında()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }

        public IActionResult Lang(string languageCode)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(languageCode)), new CookieOptions()
              {
                  Expires = DateTimeOffset.UtcNow.AddYears(1)
              });
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult DownloadCv()
        {
            var cvPath = "wwwroot/Cv/Özgeçmiş.pdf";
            var fileBytes = System.IO.File.ReadAllBytes(cvPath);
            var fileName = Path.GetFileName(cvPath);
            var contentType = "application/pdf";

            return File(fileBytes, contentType, fileName);
        }

        [HttpPost]
        public IActionResult SendMail(ContactDto request, string name,string email)
        {
            _emailService.SendMail(request);
            _emailService.SendAutoResponse(name, email); // Otomatik yanıt e-postasını gönder
            _toastNotification.AddSuccessToastMessage("Mesajınızı Aldık", new ToastrOptions { Title = "Başarılı"});
            return RedirectToAction("Index");
        }
        // Yukarı da ki işlemler cv indirmek ve mail göndermek amaçlıdır.
        // Alttaki işlemler Hakkımda sayfasının partial yöntemi ile performans amaçlı bölünmesidir.

        //public PartialViewResult Hakkimda()
        //{
        //    return PartialView();
        //}
    }
}
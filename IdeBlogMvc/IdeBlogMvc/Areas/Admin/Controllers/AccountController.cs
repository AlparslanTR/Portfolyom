using IdeBlogMvc.Areas.Admin.Dtos;
using IdentityFrameworkWepApp.Areas.Admin.Controllers;
using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;

namespace IdeBlogMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToastNotification _toastNotification;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminSignInDto request, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Anasayfa", "Home", new { area = "Admin" });

            var isUser = await _userManager.FindByEmailAsync(request.Email);
            if (isUser == null)
            {
                ModelState.AddModelError(string.Empty, "Mail veya Şifre Yanlış.!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(isUser, request.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Mail veya Şifre Yanlış.!(Başarısız Giriş Sayısı : {await _userManager.GetAccessFailedCountAsync(isUser)})" });
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "Çok Sayıda Başarısız Giriş Gerçekleşti 5 Dakika Sonra Tekrar Deneyin.!" });
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelErrorList(new List<string>() { "Bu İşleme Yetkiniz Bulunmamaktadır.!" });
            }

            return LocalRedirect(returnUrl);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AdminSignUpDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email, NormalizedEmail = request.Email }, request.PasswordConfirm);

            if (identityResult.Succeeded)
            {
                var exChangeClaim = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString()); // String olan claime kayıt olduğu tarihden itibaren 10 gün yetki veriyor.
                var userClaim = await _userManager.FindByNameAsync(request.UserName); // Kayıt olan kullanıcıyı değişkene ata.
                await _userManager.AddClaimAsync(userClaim, exChangeClaim); // Kayıt olan kullanıcıya bu claimi oto ekle.

                var user = await _userManager.FindByNameAsync(request.UserName);
                _toastNotification.AddSuccessToastMessage($"Kullanıcı {user} Başarıyla Eklendi", new ToastrOptions { CloseButton=true, Title = "Başarılı"});
                return RedirectToAction(nameof(AccountController.SignUp));
            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }
    }
}

using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Dtos;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace IdentityFrameworkWepApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public MemberController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var user =
                new UserDto
                {
                    Email = currentUser.Email,
                    UserName = currentUser.UserName,
                };
            return View(user);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var checkOldPass = await _userManager.CheckPasswordAsync(currentUser, request.oldPassword);
            if (!checkOldPass)
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreniz Yanlıştır.!");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(currentUser, request.oldPassword, request.newPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            }

            await _userManager.UpdateSecurityStampAsync(currentUser); // kullanıcının SecurityStamp özelliği güncellenir.
            await _signInManager.SignOutAsync(); // mevcut kullanıcının oturumu kapatılır. Bu işlem, güncellenen güvenlik damgasının yürürlüğe girmesi için gereklidir.
            await _signInManager.PasswordSignInAsync(currentUser, request.newPassword, true, false); // Kullanıcının yeni şifresiyle oturum açılır. Son parametre true olursa son şifre değişikliliğinden sonra sistemden atılır yeni şifreyle sisteme girilmesi istenilir. 
            TempData["SuccessMessage"] = "Şifreniz Güncellenmiştir. ";

            return View();
        }
        public async Task <IActionResult> UserEdit()
        {
            var currentUser= await _userManager.FindByNameAsync(User.Identity.Name);
            var getUserInfo = new UserEditDto()
            {
                UserName = currentUser.UserName,
                Mail = currentUser.Email
            };
            return View(getUserInfo);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser= await _userManager.FindByNameAsync(User.Identity.Name);
            currentUser.UserName= request.UserName;
            currentUser.Email = request.Mail;
            await _userManager.UpdateAsync(currentUser);

            var updateToUserResult= await _userManager.UpdateAsync(currentUser);
            if (!updateToUserResult.Succeeded)
            {
                 ModelState.AddModelErrorList(updateToUserResult.Errors.Select(x=>x.Description).ToList());
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);

            TempData["SuccessMessage"] = "Bilgileriniz Güncellendi";

            var getUserInfo = new UserEditDto()
            {
                UserName = currentUser.UserName,
                Mail = currentUser.Email,
            };
            return RedirectToAction("UserEdit", "Member");   
        }
        
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
        
        public IActionResult ClaimsList()
        {
            var userClaims=User.Claims.Select(x=>new ClaimsList() { Issuer=x.Issuer,Type=x.Type,Value=x.Value}).ToList();
            return View(userClaims);
        }
    }
}

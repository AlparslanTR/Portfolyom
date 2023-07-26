using System.ComponentModel.DataAnnotations;

namespace IdeBlogMvc.Areas.Admin.Dtos
{
    public class AdminSignUpDto
    {
        public AdminSignUpDto() 
        {

        }

        public AdminSignUpDto(string userName, string email, string password, string passwordConfirm)
        {
            UserName = userName;
            Email = email;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [EmailAddress(ErrorMessage = "Mail Formatı Yanlıştır.!")]
        [Display(Name = "*Mail :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre :")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler Uyuşmuyor.!")]
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}

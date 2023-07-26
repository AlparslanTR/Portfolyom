using System.ComponentModel.DataAnnotations;

namespace IdeBlogMvc.Areas.Admin.Dtos
{
    public class AdminSignInDto
    {
        public AdminSignInDto()
        {
        }

        public AdminSignInDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [EmailAddress(ErrorMessage = "Mail Formatı Yanlıştır.!")]
        [Display(Name = "*Mail :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre :")]
        public string Password { get; set; }
    }
}

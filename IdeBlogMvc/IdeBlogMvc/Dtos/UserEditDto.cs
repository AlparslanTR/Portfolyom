using System.ComponentModel.DataAnnotations;

namespace IdentityFrameworkWepApp.Dtos
{
    public class UserEditDto
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Kullanıcı Adı :")]
        public string UserName { get; set; }
        //
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Mail :")]
        public string Mail { get; set; }
    }
}

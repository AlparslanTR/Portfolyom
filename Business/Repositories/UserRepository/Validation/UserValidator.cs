using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail Adresi Boş Olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli Bir Mail Adresi Yazın");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Kullanıcı Resmi Boş Olamaz");

        }
    }
}

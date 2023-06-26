using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.ProjectRepository.Validation
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            // Baþlýk Kurallarý
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Proje Baþlýðý Boþ Býrakýlamaz.!");
            RuleFor(x=>x.Title).MaximumLength(50).WithMessage("Proje Baþlýðý En Fazla 50 Karakter Ýçerebilir.!");
            RuleFor(x=>x.Title).MinimumLength(20).WithMessage("Proje Baþlýðý En Az 20 Karakter Ýçerebilir.!");

            // Açýklama Kurallarý
            RuleFor(x => x.Description).NotEmpty().WithMessage("Proje Açýklamasý Boþ Býrakýlamaz.!");
        }
    }
}

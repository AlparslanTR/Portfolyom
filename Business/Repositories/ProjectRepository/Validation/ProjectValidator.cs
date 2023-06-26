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
            // Ba�l�k Kurallar�
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Proje Ba�l��� Bo� B�rak�lamaz.!");
            RuleFor(x=>x.Title).MaximumLength(50).WithMessage("Proje Ba�l��� En Fazla 50 Karakter ��erebilir.!");
            RuleFor(x=>x.Title).MinimumLength(20).WithMessage("Proje Ba�l��� En Az 20 Karakter ��erebilir.!");

            // A��klama Kurallar�
            RuleFor(x => x.Description).NotEmpty().WithMessage("Proje A��klamas� Bo� B�rak�lamaz.!");
        }
    }
}

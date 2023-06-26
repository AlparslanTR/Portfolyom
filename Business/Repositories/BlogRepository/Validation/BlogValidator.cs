using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.BlogRepository.Validation
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            // Ba�l�k Kurallar�
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Blog Ba�l��� Bo� B�rak�lamaz.!");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Blog Ba�l��� En Fazla 50 Karakter ��erebilir.!");
            RuleFor(x => x.Title).MinimumLength(20).WithMessage("Blog Ba�l��� En Az 20 Karakter ��erebilir.!");

            // A��klama Kurallar�
            RuleFor(x => x.Description).NotEmpty().WithMessage("Blog A��klamas� Bo� B�rak�lamaz.!");

            // Kategori Kurallar�
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Blog ��in Kategori Se�imi Zorunludur.!");
        }
    }
}

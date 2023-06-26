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
            // Baþlýk Kurallarý
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Blog Baþlýðý Boþ Býrakýlamaz.!");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Blog Baþlýðý En Fazla 50 Karakter Ýçerebilir.!");
            RuleFor(x => x.Title).MinimumLength(20).WithMessage("Blog Baþlýðý En Az 20 Karakter Ýçerebilir.!");

            // Açýklama Kurallarý
            RuleFor(x => x.Description).NotEmpty().WithMessage("Blog Açýklamasý Boþ Býrakýlamaz.!");

            // Kategori Kurallarý
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Blog Ýçin Kategori Seçimi Zorunludur.!");
        }
    }
}

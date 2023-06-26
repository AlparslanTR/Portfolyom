using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.BlogRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.BlogRepository
{
    public class EfBlogDal : EfEntityRepositoryBase<Blog, SimpleContextDb>, IBlogDal
    {
    }
}

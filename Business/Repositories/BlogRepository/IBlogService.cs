using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.BlogRepository
{
    public interface IBlogService
    {
        Task<IResult> Add(Blog blog);
        Task<IResult> Update(Blog blog);
        Task<IResult> Delete(Blog blog);
        Task<IDataResult<List<Blog>>> GetList();
        Task<IDataResult<Blog>> GetById(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.BlogRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.BlogRepository.Validation;
using Business.Repositories.BlogRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.BlogRepository;

namespace Business.Repositories.BlogRepository
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(BlogValidator))]
        [RemoveCacheAspect("IBlogService.Get")]

        public async Task<IResult> Add(Blog blog)
        {
            await _blogDal.Add(blog);
            return new SuccessResult(BlogMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(BlogValidator))]
        [RemoveCacheAspect("IBlogService.Get")]

        public async Task<IResult> Update(Blog blog)
        {
            await _blogDal.Update(blog);
            return new SuccessResult(BlogMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IBlogService.Get")]

        public async Task<IResult> Delete(Blog blog)
        {
            await _blogDal.Delete(blog);
            return new SuccessResult(BlogMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Blog>>> GetList()
        {
            return new SuccessDataResult<List<Blog>>(await _blogDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<Blog>> GetById(int id)
        {
            return new SuccessDataResult<Blog>(await _blogDal.Get(p => p.Id == id));
        }

    }
}

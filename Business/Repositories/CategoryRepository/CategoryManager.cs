using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.CategoryRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.CategoryRepository.Validation;
using Business.Repositories.CategoryRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.CategoryRepository;

namespace Business.Repositories.CategoryRepository
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(CategoryValidator))]
        [RemoveCacheAspect("ICategoryService.Get")]

        public async Task<IResult> Add(Category category)
        {
            await _categoryDal.Add(category);
            return new SuccessResult(CategoryMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(CategoryValidator))]
        [RemoveCacheAspect("ICategoryService.Get")]

        public async Task<IResult> Update(Category category)
        {
            await _categoryDal.Update(category);
            return new SuccessResult(CategoryMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("ICategoryService.Get")]

        public async Task<IResult> Delete(Category category)
        {
            await _categoryDal.Delete(category);
            return new SuccessResult(CategoryMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Category>>> GetList()
        {
            return new SuccessDataResult<List<Category>>(await _categoryDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<Category>> GetById(int id)
        {
            return new SuccessDataResult<Category>(await _categoryDal.Get(p => p.Id == id));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.ProjectRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.ProjectRepository.Validation;
using Business.Repositories.ProjectRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.ProjectRepository;

namespace Business.Repositories.ProjectRepository
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectDal _projectDal;

        public ProjectManager(IProjectDal projectDal)
        {
            _projectDal = projectDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(ProjectValidator))]
        [RemoveCacheAspect("IProjectService.Get")]

        public async Task<IResult> Add(Project project)
        {
            await _projectDal.Add(project);
            return new SuccessResult(ProjectMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(ProjectValidator))]
        [RemoveCacheAspect("IProjectService.Get")]

        public async Task<IResult> Update(Project project)
        {
            await _projectDal.Update(project);
            return new SuccessResult(ProjectMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IProjectService.Get")]

        public async Task<IResult> Delete(Project project)
        {
            await _projectDal.Delete(project);
            return new SuccessResult(ProjectMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Project>>> GetList()
        {
            return new SuccessDataResult<List<Project>>(await _projectDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<Project>> GetById(int id)
        {
            return new SuccessDataResult<Project>(await _projectDal.Get(p => p.Id == id));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.ProjectRepository
{
    public interface IProjectService
    {
        Task<IResult> Add(Project project);
        Task<IResult> Update(Project project);
        Task<IResult> Delete(Project project);
        Task<IDataResult<List<Project>>> GetList();
        Task<IDataResult<Project>> GetById(int id);
    }
}

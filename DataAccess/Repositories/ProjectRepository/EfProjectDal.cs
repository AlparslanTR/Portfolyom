using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.ProjectRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.ProjectRepository
{
    public class EfProjectDal : EfEntityRepositoryBase<Project, SimpleContextDb>, IProjectDal
    {
    }
}

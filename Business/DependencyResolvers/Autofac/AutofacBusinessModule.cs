using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Authentication;
using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository;
using Business.Repositories.UserRepository;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using Business.Repositories.ProjectRepository;
using DataAccess.Repositories.ProjectRepository;
using Business.Repositories.BlogRepository;
using DataAccess.Repositories.BlogRepository;
using Business.Repositories.CategoryRepository;
using DataAccess.Repositories.CategoryRepository;
using DataAccess.Repositories.UserRepository;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<ProjectManager>().As<IProjectService>().SingleInstance();
            builder.RegisterType<EfProjectDal>().As<IProjectDal>().SingleInstance();

            builder.RegisterType<BlogManager>().As<IBlogService>().SingleInstance();
            builder.RegisterType<EfBlogDal>().As<IBlogDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}

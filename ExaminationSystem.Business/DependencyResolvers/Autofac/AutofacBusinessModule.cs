using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Concrete;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.Security.User;

namespace ExaminationSystem.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<QuestionManager>().As<IQuestionService>();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>();

            builder.RegisterType<NoteManager>().As<INoteService>();
            builder.RegisterType<EfNoteDal>().As<INoteDal>();

            builder.RegisterType<ExamParameterManager>().As<IExamParameterService>();
            builder.RegisterType<EfExamParameterDal>().As<IExamParameterDal>();

            builder.RegisterType<SolvedQuestionManager>().As<ISolvedQuestionService>();
            builder.RegisterType<EfSolvedQuestionDal>().As<ISolvedQuestionDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserAccessor>().As<IUserAccessor>();


            builder.RegisterType<ClassLevelManager>().As<IClassLevelService>();
            builder.RegisterType<EfClassLevelDal>().As<IClassLevelDal>();

            //builder.RegisterType<ExaminationSystemContext>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
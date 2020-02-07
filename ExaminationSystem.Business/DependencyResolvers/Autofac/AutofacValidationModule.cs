using Autofac;
using ExaminationSystem.Business.ValidationRules.FluentValidation;
using ExaminationSystem.Models.Entities;
using FluentValidation;

namespace ExaminationSystem.Business.DependencyResolvers.Autofac
{
    public class AutofacValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryValidator>().As(typeof(IValidator<Category>));
            builder.RegisterType<ClassLevelValidator>().As(typeof(IValidator<ClassLevel>));
            builder.RegisterType<QuestionValidator>().As(typeof(IValidator<Question>));
        }
    }
}

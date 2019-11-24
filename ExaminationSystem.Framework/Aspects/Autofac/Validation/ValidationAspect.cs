using Castle.DynamicProxy;
using ExaminationSystem.Framework.CrossCuttingConcerns.Validation.FluentValidation;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.Messages;
using FluentValidation;
using System;
using System.Linq;

namespace ExaminationSystem.Framework.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        //Fluent VAlidation ve Autofac kullanıldı.

        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(x => x.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
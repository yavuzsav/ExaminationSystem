using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;
using ExaminationSystem.Framework.Aspects.Autofac.Exception;
using ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace ExaminationSystem.Framework.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttribute = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttribute.AddRange(methodAttributes);
            classAttribute.Add(new ExceptionLogAspect(typeof(FileLogger)));
            classAttribute.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));

            return classAttribute.OrderBy(x => x.Priority).ToArray();
        }
    }
}
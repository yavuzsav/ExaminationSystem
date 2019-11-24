using Castle.DynamicProxy;
using ExaminationSystem.Framework.CrossCuttingConcerns.Caching;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace ExaminationSystem.Framework.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
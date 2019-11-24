using ExaminationSystem.Framework.CrossCuttingConcerns.Caching;
using ExaminationSystem.Framework.CrossCuttingConcerns.Caching.Microsoft;
using ExaminationSystem.Framework.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace ExaminationSystem.Framework.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
using Castle.DynamicProxy;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExaminationSystem.Business.BusinessAspects.Autofac
{
    public class AuthenticationAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new Exception(Messages.NotAuthorize);
            }
        }
    }
}
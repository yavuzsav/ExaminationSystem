using Castle.DynamicProxy;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Framework.Extensions;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExaminationSystem.Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
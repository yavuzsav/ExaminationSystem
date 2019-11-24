using Castle.DynamicProxy;
using ExaminationSystem.Framework.CrossCuttingConcerns.Logging;
using ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net;
using ExaminationSystem.Framework.Utilities.Interceptors.Autofac;
using ExaminationSystem.Framework.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace ExaminationSystem.Framework.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new System.Exception(AspectMessages.WrongLoggerType);

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            //yada dependency inject ile de loglama türüne karar verilebilir.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetail;
        }
    }
}
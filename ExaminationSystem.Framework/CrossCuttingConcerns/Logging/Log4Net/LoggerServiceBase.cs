using log4net;
using log4net.Repository;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private readonly ILog _log;

        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);
        }

        public bool IsInfoEnable => _log.IsInfoEnabled;
        public bool IsDebugEnable => _log.IsDebugEnabled;
        public bool IsWarnEnable => _log.IsWarnEnabled;
        public bool IsFatalEnable => _log.IsFatalEnabled;
        public bool IsErrorEnable => _log.IsErrorEnabled;

        public void Info(object logMessage)
        {
            if (IsInfoEnable)
                _log.Info(logMessage);
        }

        public void Debug(object logMessage)
        {
            if (IsDebugEnable)
                _log.Debug(logMessage);
        }

        public void Warn(object logMessage)
        {
            if (IsWarnEnable)
                _log.Warn(logMessage);
        }

        public void Fatal(object logMessage)
        {
            if (IsFatalEnable)
                _log.Fatal(logMessage);
        }

        public void Error(object logMessage)
        {
            if (IsErrorEnable)
                _log.Error(logMessage);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Web;

namespace YOYO.HRMS.Utility
{
    public class NlogLogger : ILogger
    {
        private Logger _logger;

        public NlogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.ErrorException(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        public void LogOperation(string corporateID, string userID, string userCode, string controlName, string actionName, string desc, string message, string lastSql)
        {
            LogEventInfo ei = new LogEventInfo(LogLevel.Info, "", "");
            ei.Properties["corporateID"] = corporateID;
            ei.Properties["userID"] = userID;
            ei.Properties["userCode"] = userCode;
            ei.Properties["clientHost"] = HttpContext.Current.Request.UserHostName;
            ei.Properties["clientIP"] = HttpContext.Current.Request.UserHostAddress;
            ei.Properties["controllerName"] = controlName;
            ei.Properties["actionName"] = actionName;
            ei.Properties["operationDesc"] = desc;
            ei.Properties["logMsg"] = message;
            ei.Properties["lastSql"] = lastSql;

            _logger.Log(ei);
        }


        public void LogException(string corporateID, string userID, string userCode, string controlName, string actionName, string desc, string lastSql, Exception x)
        {
            LogEventInfo ei = new LogEventInfo(LogLevel.Error, "", "");
            ei.Properties["corporateID"] = corporateID;
            ei.Properties["userID"] = userID;
            ei.Properties["userCode"] = userCode;
            ei.Properties["clientHost"] = HttpContext.Current.Request.UserHostName;
            ei.Properties["clientIP"] = HttpContext.Current.Request.UserHostAddress;
            ei.Properties["controllerName"] = controlName;
            ei.Properties["actionName"] = actionName;
            ei.Properties["operationDesc"] = desc;
            ei.Properties["lastSql"] = lastSql;
            ei.Properties["exceptionMsg"] = LogUtility.BuildExceptionMessage(x);
            ei.Properties["allXml"] = LogUtility.BuildWebVariablesMessage();

            _logger.Log(ei);
        }
    }
}

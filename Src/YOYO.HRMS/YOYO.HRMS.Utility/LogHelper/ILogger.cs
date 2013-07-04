using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YOYO.HRMS.Utility
{
    public interface ILogger
    {
        void Info(string message);

        void Warn(string message);

        void Debug(string message);

        void Error(string message);
        void Error(string message, Exception x);
        void Error(Exception x);

        void Fatal(string message);
        void Fatal(Exception x);

        void LogOperation(string corporateID,string userID, string userCode, string controlName, string actionName, string desc, string message,string LastSql);

        void LogException(string corporateID, string userID, string userCode,  string controlName, string actionName, string desc, string LastSql, Exception x);


    }
}

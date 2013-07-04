using System.Web.Mvc;
using System.Web;

using YOYO.HRMS.MVC.CustomAttributes;
using YOYO.HRMS.Utility;
using YOYO.HRMS.Models;
using YOYO.HRMS.BusinessLogic;
using System;

namespace YOYO.HRMS.MVC.Controllers
{
    /// <summary>
    /// Controller的基类
    /// Author：Paul Zhou   Date：2012/11/15
    /// 说明：   
    ///   ControllerBase作为项目中所有Controller的基类
    /// Update Log:
    /// 
    /// </summary>
    [PermissionFilter]
    public class BaseController : Controller
    {
        private ILogger _logger;
        private string _userID;
        private string _userCode;
        private string _corporateID;

        protected ILogger Logger
        {
            get
            {
                return _logger ??
                    (_logger = DependencyResolver.Current.GetService<ILogger>());
            }
        }

        protected string UserID
        {
            get
            {
                return _userID ??
                    (_userID = CurrentParemeter.GetuserID().ToString());
            }
        }

        protected string UserCode
        {
            get
            {
                return _userCode ??
                    (_userCode = CurrentParemeter.GetCurrentUser().UserCode);
            }
        }


        protected string CorporateID
        {
            get
            {
                return _corporateID ??
                    (_corporateID = CurrentParemeter.GetCurrentCorporateId().ToString());
            }
        }

        /// <summary>
        /// 记录用户操作记录
        /// </summary>
        /// <param name="controllerName">controller名称</param>
        /// <param name="actionName">action名称</param>
        /// <param name="desc">action描述</param>
        /// <param name="msg">日志信息</param>
        /// <param name="lastSql">sql</param>
        protected void LogOP(string controllerName, string actionName, string desc, string msg, string lastSql)
        {


            Logger.LogOperation(CorporateID, UserID, UserCode, controllerName, actionName, desc, msg, lastSql);
        }

        /// <summary>
        /// 记录Exception信息
        /// </summary>
        /// <param name="controllerName">controller名称</param>
        /// <param name="actionName">action名称</param>
        /// <param name="desc">action描述</param>
        /// <param name="lastSql">sql</param>
        /// <param name="ex">Exception</param>
        protected void LogException(string controllerName, string actionName, string desc, string lastSql, Exception ex)
        {
            Logger.LogException(CorporateID, UserID, UserCode, controllerName, actionName, desc, lastSql,ex);
        }
    }
}

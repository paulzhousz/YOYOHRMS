using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Globalization;

using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.BusinessLogic.OrganizationManagement;
using YOYO.HRMS.MVC.CustomAttributes;
using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.MVC.Controllers;
using YOYO.HRMS.MVC.ViewModels;
using YOYO.HRMS.Utility;
using YOYO.HRMS.MVC;

namespace YOYO.HRMS.Web.Controllers
{
    public class HomeController :BaseController
    {
        IUserService _userService;
        ILocalizedViewService _localizedViewService;
        ICorporateService _corpService;

        public HomeController(IUserService userservice, ICorporateService corpservice, ILocalizedViewService localizedViewService)
        {
            _userService = userservice;
            _corpService = corpservice;
            _localizedViewService = localizedViewService;
        }

        //
        // GET: /Home/
        [Description("系统首页")]
        [LoginAllowView]
        [ViewPage]
        public ActionResult Index()
        {
            return View();
        }

        [Description("Tab首页")]
        [LoginAllowView]
        [ViewPage]
        public ActionResult TabIndex()
        {
            return View();
        }

        [Anonymous]
        [Description("系统登陆页面")]
        [ViewPage]
        public ActionResult Login()
        {
            //初始化 Theme
            var themeValue = CookieHelper.GetCookieValue("YoYoThemeName");
            if (themeValue == string.Empty)
            {
                CurrentParemeter.SetCurrentTheme("bootstrap");
            }


            return View();
        }

        [Description("login登录,登录成功则跳转")]
        [Anonymous]    
        public ActionResult LoginAndRedirect()
        {
            UserRequest request = new UserRequest(HttpContext);
            bool isError=true;
            var result = _userService.LoginVerify(request.CorporateID, request.UserCode, request.UserPwd);
            var message= FriendlyMessage.ToMessage(result);
            if (result == UserMessage.UserLoginSuccess)
            {
                isError = false;
                CurrentParemeter.SetCurrentCorp(request.CorporateID);
                CurrentParemeter.SetCurrentUser(request.UserCode);

                //写操作日志
                string logmsg = string.Format("用户[{0}]登陆[{1}]成功！",
                    CurrentParemeter.GetCurrentUser().UserCode,
                    CurrentParemeter.GetCurrentCorporate().CorporateCode);

                string controlName = RouteData.Values["controller"].ToString();
                string actionName = RouteData.Values["action"].ToString();
                string desc = "";
                
               
                LogOP(controlName, actionName, desc, logmsg, "");
            }
            else
            {
                isError = true;
            }
            //var data = new { Result = status,
            //    Title=_localizedViewService.LoadCommonPrompt(request.CorporateID,"Error"),
            //    Message = FriendlyMessage.ToMessage(result) };
            //return Json(data,"text/html");
            return this.JsonFormat(isError, isError, message);
           // return this.JsonFormat(status,!status, FriendlyMessage.ToMessage(result));         
        }

        [Description("获取公司列表，供login页面调用")]
        [Anonymous]    
        public ActionResult GetCorporateList()
        {
            IEnumerable<Corporate> corpList = _corpService.GetAllCorporate();
            var items = from n in corpList
                     select new
                     {
                         corporateID = n.CorporateID,
                         corporateName = "[" + n.CorporateCode + "]" + n.CorporateName
                     };
            //var result = Json(items, JsonRequestBehavior.AllowGet);
            var result = this.JsonFormat(items);
            return result;
        }

        [Description("更改系统语言")]
        [LoginAllowView]
        [HttpPost]
        public ActionResult ChangeLanguage(string languName)
        {
            bool status = false;
            try
            {
                CurrentParemeter.SetCurrentLan(languName);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            var data = new { Result = status };
            return Json(data, "text/html");
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using YOYO.HRMS.BusinessLogic;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.BusinessLogic.OrganizationManagement;
using YOYO.HRMS.MVC.CustomAttributes;
using YOYO.HRMS.Models;
using YOYO.HRMS.MVC.Controllers;
using YOYO.HRMS.Web.ViewModels;
using YOYO.HRMS.Utility;

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

        [Anonymous]
        [ViewPage]
        public ActionResult Login()
        {
            //初始化 Theme和语言
            var themeValue = CookieHelper.GetCookieValue("YoYoThemeName");
            if (themeValue == string.Empty)
            {
                CurrentParemeter.SetCurrentTheme("bootstrap");
            }

            var lanValue = CookieHelper.GetCookieValue("YoYoLanguage");
            if (lanValue == string.Empty)
            {
                var clientLang = Request.UserLanguages[0];
                CurrentParemeter.SetCurrentLan(clientLang);

            }

            return View();
        }

        [Description("login登录,登录成功则跳转")]
        [Anonymous]    
        public ActionResult LoginAndRedirect()
        {
            UserViewModel model = new UserViewModel(HttpContext);
            bool status=false;
            var result = _userService.LoginVerify(model.CorporateID, model.UserCode, model.UserPwd);
            if (result == UserMessage.UserLoginSuccess)
            {
                status = true;
                CurrentParemeter.SetCurrentCorp(model.CorporateID);
                CurrentParemeter.SetCurrentUser(model.UserCode);
            }
            else
            {
                status = false;
            }
            var data = new { Result = status,
                Title=_localizedViewService.LoadCommonPrompt(model.CorporateID,"Error"),
                Message = FriendlyMessage.ToMessage(result) };
            return Json(data,"text/html");
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
            var result = Json(items, JsonRequestBehavior.AllowGet);
            return result;
        }

    }
}

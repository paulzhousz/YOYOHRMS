using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YOYO.HRMS.MVC.Controllers;
using YOYO.HRMS.MVC.CustomAttributes;
using System.ComponentModel;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.MVC;
using YOYO.HRMS.MVC.ViewModels;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.Web.SystemManagement.Areas.SystemManagement.Controllers
{
    public class UserManageController : BaseController
    {
        private IUserService _userService;
        private ILocalizedViewService _localizedviewService;

        public UserManageController(IUserService userService, ILocalizedViewService localizedviewService)
        {
            _userService = userService;
            _localizedviewService = localizedviewService;
        }
        //
        // GET: /SystemManagement/UserManage/

        public ActionResult Index()
        {
            return View();
        }

        [LoginAllowView]
        [Description("修改密码弹窗")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Description("修改用户密码")]
        [LoginAllowView]
        [HttpPost]
        public ActionResult ChangeUserPassword()
        {
            UserRequest request = new UserRequest(HttpContext);
            var user = CurrentParemeter.GetCurrentUser();
            var status = false;
            var md5Pwd = MD5Provider.Hash(request.OldUserPwd);
            if (md5Pwd != user.UserPwd)
            {
                return this.JsonFormatError(T("PwdError", true));
            }

            status = _userService.ChangePassword(user.UserID, request.NewUsrPwd);
            //TODO:test
            return this.JsonFormat(status, status, SysOperate.Operate);            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YOYO.HRMS.MVC.ViewModels
{
    /// <summary>
    /// 用于传递用户登录及修改密码的前台request参数
    /// </summary>
    public class UserRequest
    {
        public UserRequest(){}

        public UserRequest(HttpContextBase context)
        {
            CorporateID = long.Parse(context.Request["corporateList"]);
            UserCode = context.Request["usercode"];
            UserPwd = context.Request["userpwd"];
            OldUserPwd = context.Request["oldPassword"];
            NewUsrPwd = context.Request["newPassword"];
        }

        
        public long UserID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string OldUserPwd { get; set; }
        public string NewUsrPwd { get; set; }
        public string MailAddress { get; set; }
        public bool IsEnabled { get; set; }
        public long CorporateID { get; set; }

    }
}
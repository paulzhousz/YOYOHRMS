using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using YOYO.HRMS.MVC.ViewModel;

namespace YOYO.HRMS.Web.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        public UserViewModel(HttpContextBase context)
        {
            CorporateID = long.Parse(context.Request["corporateList"]);
            UserCode = context.Request["usercode"];
            UserPwd = context.Request["userpwd"];
        }

        
        public long UserID { get; set; }
        [Required]
        public string UserCode { get; set; }
        public string UserName { get; set; }
        [Required]
        public string UserPwd { get; set; }
        public string OldUserPwd { get; set; }
        public string NewUsrPwd { get; set; }
        public string MailAddress { get; set; }
        public bool IsEnabled { get; set; }        


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YOYO.HRMS.MVC.Controllers;
using YOYO.HRMS.MVC.CustomAttributes;
using System.ComponentModel;

namespace YOYO.HRMS.Web.SystemManagement.Areas.SystemManagement.Controllers
{
    public class UserManageController : Controller
    {
        //
        // GET: /SystemManagement/UserManage/

        public ActionResult Index()
        {
            return View();
        }

    }
}

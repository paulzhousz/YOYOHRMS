using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YOYO.HRMS.Web.SystemManagement.Areas.SystemManagement.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SystemManagement/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}

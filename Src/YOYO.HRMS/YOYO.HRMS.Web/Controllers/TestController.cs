using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YOYO.HRMS.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetFormData()
        {
            var s = Request.Form.Count;
            var a1 = Request.Form["a1"];
            var a2 = Request.Form["a2"];
            return Content(a1+a2);
        }

    }
}

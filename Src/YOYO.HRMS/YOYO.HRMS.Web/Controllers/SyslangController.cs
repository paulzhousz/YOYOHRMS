using System.Globalization;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic.SystemManagement;

namespace YOYO.HRMS.Web.Controllers
{
    public class SyslangController : Controller
    {
        private readonly ISysLanService _lanService;
        
        public SyslangController(ISysLanService service)
        {
            _lanService = service;

            

        }
        //
        // GET: /Syslang/

        public ActionResult Index()
        {
            var culture= _lanService.GetCurrentLang(CultureInfo.CurrentCulture.Name);
            ViewBag.culture = culture.Name;
            return View();
        }

    }
}

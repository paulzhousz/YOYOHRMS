using System.Threading;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.BusinessLogic;

namespace YOYO.HRMS.MVC.CustomAttributes
{
    public class LanguageFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var lanService = DependencyResolver.Current.GetService<ISysLanService>();
            string cultureName = null;
            var cultureCookie = request.Cookies["_culture"];
            if (request.UserLanguages != null)
                cultureName = cultureCookie != null ? cultureCookie.Value : request.UserLanguages[0];
            var currentCulture = lanService.GetCurrentLang(CurrentParemeter.GetCurrentCorporateId(), cultureName);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.OnActionExecuting(filterContext);
        }
    }
}
using System.Web.Mvc;

namespace YOYO.HRMS.Web.SystemManagement
{
    public class GlobalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemManagement_default",
                "SystemManagement/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "YOYO.HRMS.Web.SystemManagement.Areas.SystemManagement.Controllers" }
            );
        }
    }
}

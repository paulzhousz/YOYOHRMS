using System.Web.Mvc;

namespace YOYO.HRMS.Web.OrganizationManagement
{
    public class GlobalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "OrganizationManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "OrganizationManagement_default",
                "OrganizationManagement/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "YOYO.HRMS.Web.OrganizationManagement.Areas.OrganizationManagement.Controllers" }
            );
        }
    }
}

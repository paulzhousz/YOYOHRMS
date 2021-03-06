﻿using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;

using YOYO.HRMS.MVC.Provider;
using YOYO.HRMS.Utility;
using YOYO.HRMS.BusinessLogic;

namespace YOYO.HRMS.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("JSPrompts", "scripts/JSPrompts.js", new { controller = "Language", action = "Language" });
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Login", id = UrlParameter.Optional }, // 参数默认值
                new[] { "YOYO.HRMS.Web.Controllers" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Repository & Serveric IOC
            Bootstrapper.Initialise();

            //多语言处理程序注册

            ModelMetadataProviders.Current = new LocalizedModelMetadataProvider();

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new LocalizedModelValidatorProvider());


        }
    }
}
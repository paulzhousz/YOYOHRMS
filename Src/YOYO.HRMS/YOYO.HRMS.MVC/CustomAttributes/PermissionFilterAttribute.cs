using System;
using System.Linq;
using System.Web.Mvc;

using YOYO.HRMS.BusinessLogic;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.MVC.Provider;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.MVC.CustomAttributes
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {

        private readonly string[] _allowPage = { "/Home/Login", "/Error" };
        
        /// <summary>
        /// 权限拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限拦截是否忽略
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var path = filterContext.HttpContext.Request.Path.ToLower();

            bool isIgnored = _allowPage.Any(page => page.ToLower() == path);
            if (isIgnored)
                return;
            //接下来进行权限拦截与验证
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewPageAttribute), true);
            var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

            if (AuthorizeCore(filterContext) == false)
            {
                if (isViewPage)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Login");
                }
                else
                {
                    {
                        var attrsUIException =
                            filterContext.ActionDescriptor.GetCustomAttributes(typeof (EasyUIExceptionResultAttribute),
                                                                               true);
                        if (attrsUIException.Length == 1)
                        {
                            filterContext.Result = new FormatJsonResult
                                {
                                    IsError = true,
                                    Data = null,
                                   // Message = "您没有权限执行此操作！"
                                   Message = ValidationMessageProviders.GetCommonMessage("NoPriviledge")
                                }; //功能权限弹出提示框
                        }
                        else

                            filterContext.RequestContext.HttpContext.Response.Redirect("/Error");
                    }
                }
            }
            //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
        }

        /// <summary>
        /// 用户业务权限判断
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool AuthorizeCore(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            //验证当前Action是否是匿名访问Action
            if (CheckAnonymous(filterContext))
                return true;
            //未登录验证
            if (CurrentParemeter.GetuserID()==-1)
            {
                return false;
            }
            //验证当前Action是否是登录就可以访问的Action
            if (CheckLoginAllowView(filterContext))
                return true;
            return true;
        }


        /// <summary>
        /// [LoginAllowView标记]验证是否登录就可以访问(如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool CheckLoginAllowView(ActionExecutingContext filterContext)
        {
            //在这里允许一种情况,如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginAllowViewAttribute), true);
            //是否是LoginAllowView
            var viewMethod = attrs.Length == 1;
            return viewMethod;
        }

        /// <summary>
        /// /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool CheckAnonymous(ActionExecutingContext filterContext)
        {
            //验证是否是匿名访问的Action
            var attrsAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AnonymousAttribute), true);
            //是否是Anonymous
            var anonymous = attrsAnonymous.Length == 1;
            return anonymous;
        }
    }
}
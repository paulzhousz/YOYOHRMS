using System;

namespace YOYO.HRMS.MVC.CustomAttributes
{
    /// <summary>
    /// 代表该方法可以允许登录用户都能访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginAllowViewAttribute : Attribute
    {
    }
}
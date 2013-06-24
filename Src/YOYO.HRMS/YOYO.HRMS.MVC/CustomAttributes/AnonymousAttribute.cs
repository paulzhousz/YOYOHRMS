using System;

namespace YOYO.HRMS.MVC.CustomAttributes
{
    /// <summary>
    /// 匿名访问标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AnonymousAttribute : Attribute
    {
    }
}
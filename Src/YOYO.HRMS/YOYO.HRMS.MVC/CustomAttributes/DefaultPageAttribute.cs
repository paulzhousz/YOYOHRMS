using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YOYO.HRMS.MVC.CustomAttributes
{
    /// <summary>
    /// 控制器默认的首页
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultPageAttribute : Attribute
    {

    }
}

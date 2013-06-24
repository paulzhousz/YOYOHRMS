using System.Web.Mvc;
using YOYO.HRMS.MVC.CustomAttributes;

namespace YOYO.HRMS.MVC.Controllers
{
    /// <summary>
    /// Controller的基类
    /// Author：Paul Zhou   Date：2012/11/15
    /// 说明：   
    ///   ControllerBase作为项目中所有Controller的基类
    /// Update Log:
    /// 
    /// </summary>
    [PermissionFilter]
    public class BaseController:Controller
    {
    }
}

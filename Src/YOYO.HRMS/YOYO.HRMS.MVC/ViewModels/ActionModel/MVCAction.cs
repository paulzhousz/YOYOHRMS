using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YOYO.HRMS.MVC.ViewModels
{
    /// <summary>
    /// 动作
    /// </summary>
    public class MVCAction
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

    }
}

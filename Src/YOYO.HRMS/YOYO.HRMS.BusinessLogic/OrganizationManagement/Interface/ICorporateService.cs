using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Models;
using YOYO.HRMS.DataAccess;

namespace YOYO.HRMS.BusinessLogic.OrganizationManagement
{
    public interface ICorporateService:IBaseService<Corporate>
    {
        /// <summary>
        /// 获取所有可以登入的公司列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Corporate> GetAllCorporate();
    }
}

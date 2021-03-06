﻿using System.Globalization;
using System.Collections.Generic;

using YOYO.HRMS.Models;



namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public interface ISysLanService:IBaseService<SysLang>
    {
        CultureInfo GetCurrentLang(long corporateID,string cultureName);
        CultureInfo GetDefaultLang();
        IEnumerable<SysLang> GetLangList();
    }
}
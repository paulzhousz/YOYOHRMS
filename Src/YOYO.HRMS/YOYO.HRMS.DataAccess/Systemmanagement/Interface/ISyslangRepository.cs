using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Models;
using System.Collections.Generic;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public interface ISyslangRepository:IRepository<SysLang>
    {
        IEnumerable<SysLang> GetLangs(long corporateId);
    }
}

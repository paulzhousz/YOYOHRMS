using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YOYO.HRMS.MVC.ViewModel
{
    /// <summary>
    /// ViewModel基类
    /// </summary>
   public class BaseViewModel
    {
       public bool? IsDeleted { get; set; }
       public long  CorporateID { get; set; }
       public string Created { get; set; }
       public DateTime? CreatDate { get; set; }
       public string Updated { get; set; }
       public DateTime? UpdateDate { get; set; }

    }
}

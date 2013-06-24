using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess.OrganizationManagement
{
    public class CorporateRepository:PetaPocoRepository<Corporate>,ICorporateRepository
    {
        public CorporateRepository(IDBFactory dbFactory):base(dbFactory){}
    }
}

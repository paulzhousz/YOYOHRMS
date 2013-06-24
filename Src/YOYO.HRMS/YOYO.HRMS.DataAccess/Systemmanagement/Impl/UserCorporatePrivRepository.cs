using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public class UserCorporatePrivRepository:PetaPocoRepository<UserCorporatePriv>,IUserCorporatePrivRepository
    {
        public UserCorporatePrivRepository(IDBFactory dbFactory) : base(dbFactory) { }
    }
}

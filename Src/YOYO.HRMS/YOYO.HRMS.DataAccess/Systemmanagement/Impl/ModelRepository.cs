using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public class ModelRepository:PetaPocoRepository<Module>,IModelRepository
    {
        public ModelRepository(IDBFactory dbFactory):base(dbFactory){}
    }
}

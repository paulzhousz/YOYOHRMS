using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess
{
    public class PetaPocoUnitOfWork : IUnitOfWork
    {
        private YOYOHRMSDB _dbContext;
        private readonly IDBFactory _dbFactory;


        public YOYOHRMSDB DBContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Get() as YOYOHRMSDB); }
        }
        public PetaPocoUnitOfWork(IDBFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public void Commit()
        {
            DBContext.CompleteTransaction();
        }

        public void Rollback()
        {
            DBContext.AbortTransaction();           
        }


        public void StartNew()
        {
            DBContext.BeginTransaction();           
        }
    }
}

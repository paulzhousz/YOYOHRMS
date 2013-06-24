using System;
using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess
{
    public class PetaPocoDBFactory : IDBFactory, IDisposable
    {
        private YOYOHRMSDB _dbContext;
        public Object Get()
        {
            return _dbContext ?? (_dbContext = YOYOHRMSDB.GetInstance());            
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}

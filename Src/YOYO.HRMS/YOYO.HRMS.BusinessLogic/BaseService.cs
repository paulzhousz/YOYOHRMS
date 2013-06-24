using System;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.DataAccess;
using System.Collections.Generic;
using YOYO.HRMS.Utility;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.BusinessLogic
{
    public class BaseService<T>:IBaseService<T>
    {

        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _uow;

        protected readonly long currentCorporateId;
        protected readonly string defautFilter; 

        public BaseService(IUnitOfWork uow, IRepository<T> repository)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (repository == null) throw new ArgumentNullException("repository");
            _uow = uow;
            _repository = repository;

            var _currentCrop = SessionHelper.GetSession("currentCorp");
            if (_currentCrop!=null)
            {
                defautFilter = string.Format("where c_corporateid={0} ",((Corporate)_currentCrop).CorporateID);
            }
            else
            {
                defautFilter = "where 1=1 ";
            }
        }

        public IEnumerable<TPassType> Query<TPassType>()
        {            
            return _repository.Query<TPassType>(defautFilter);
        }

        public IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args)
        {
            sql = defautFilter + sql;
           return _repository.Query<TPassType>(sql, args);
        }

        public IEnumerable<TPassType> Query<TPassType>(string sql)
        {
            sql = defautFilter + sql;
            return _repository.Query<TPassType>(sql);
        }

        public TPassType GetEntity<TPassType>(object primaryKey)
        {
            return _repository.SingleOrDefault<TPassType>(primaryKey);
        }

        public TPassType GetEntity<TPassType>(string sql, params object[] args)
        {
            sql = defautFilter + sql;
            return _repository.SingleOrDefault<TPassType>(sql, args);
        }

        public TPassType GetEntity<TPassType>(string sql)
        {
            sql = defautFilter + sql;
            return _repository.SingleOrDefault<TPassType>(sql);
        }

        public List<TPassType> Fetch<TPassType>()
        {
            return _repository.Fetch<TPassType>(defautFilter);
        }

        public List<TPassType> Fetch<TPassType>(string sql)
        {
            sql = defautFilter + sql;
            return _repository.Fetch<TPassType>(sql);
        }

        public List<TPassType> Fetch<TPassType>(string sql, params object[] args)
        {
            sql = defautFilter + sql;
            return _repository.Fetch<TPassType>(sql,args);
        }

        public int Insert(object poco)
        {
            return _repository.Insert(poco);
        }

        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return _repository.Insert(tableName, primaryKeyName, autoIncrement, poco);
        }

        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return _repository.Insert(tableName, primaryKeyName, poco);
        }

        public int Update(object poco)
        {
            return _repository.Update(poco);
        }

        public int Update(object poco, object primaryKeyValue)
        {
            return _repository.Update(poco, primaryKeyValue);
        }

        public int Update(string tableName, string primaryKeyName, object poco)
        {
            return _repository.Update(tableName, primaryKeyName, poco);
        }

        public int Update(object poco, IEnumerable<string> columns)
        {
            return _repository.Update(poco, columns);
        }

        public int Update<TPassType>(string sql)
        {
            sql = defautFilter + sql;
            return _repository.Update<TPassType>(sql);
        }

        public int Update<TPassType>(string sql, params object[] args)
        {
            sql = defautFilter + sql;
            return _repository.Update<TPassType>(sql,args);
        }

        public int Delete<TPassType>(object pocoOrPrimaryKey)
        {
            return _repository.Delete<TPassType>(pocoOrPrimaryKey);
        }

        public int Delete<TPassType>(string sql)
        {
            sql = defautFilter + sql;
            return _repository.Delete<TPassType>(sql);
        }

        public int Delete<TPassType>(string sql, params object[] args)
        {
            sql = defautFilter + sql;
            return _repository.Delete<TPassType>(sql);
        }

        public List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql)
        {            
            sql = defautFilter + sql;
            return _repository.PagedQuery<TPassType>(pageNumber, itemsPerPage, out totalItems, out totalPages, sql);
        }

        public List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql, params object[] args)
        {
            sql = defautFilter + sql;
            return _repository.PagedQuery<TPassType>(pageNumber, itemsPerPage, out totalItems, out totalPages, sql,args);
        }


        public long GetCounts<TPassType>(string sql, params object[] args)
        {
            return _repository.GetCounts<TPassType>(defautFilter + sql, args);
        }

        public long GetCounts<TPassType>(string sql)
        {
            return _repository.GetCounts<TPassType>(defautFilter + sql);
        }

        public bool Exists<TPassType>(string sql, params object[] args)
        {
            return _repository.Exists<TPassType>(defautFilter + sql, args); ;
        }

        public bool Exists<TPassType>(string sql)
        {
           return _repository.Exists<TPassType>(defautFilter);
        }
    }
}
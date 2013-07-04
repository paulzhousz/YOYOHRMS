using System;
using System.Collections.Generic;
using PetaPoco;
using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess
{
    public class PetaPocoRepository<T> : IRepository<T> where T : class
    {
        private YOYOHRMSDB _dbcontext;

        protected IDBFactory DBFactory
        {
            get;
            private set;
        }

        public PetaPocoRepository(IDBFactory dbFactory)
        {
            DBFactory = dbFactory;
        }

        protected YOYOHRMSDB DBContext
        {
            get { return _dbcontext ?? DBFactory.Get() as YOYOHRMSDB; }
        }

        public TPassType Single<TPassType>(object primaryKey)
        {
            return DBContext.Single<TPassType>(primaryKey);
        }

        public TPassType Single<TPassType>(string sql, params object[] args)
        {
            return DBContext.Single<TPassType>(sql,args);
        }

        public TPassType Single<TPassType>(string sql)
        {
            return DBContext.Single<TPassType>(sql);
        }

        public TPassType SingleOrDefault<TPassType>(object primaryKey)
        {
            return DBContext.SingleOrDefault<TPassType>(primaryKey);
        }

        public TPassType SingleOrDefault<TPassType>(string sql, params object[] args)
        {
            return DBContext.SingleOrDefault<TPassType>(sql, args);
        }

        public TPassType SingleOrDefault<TPassType>(string sql)
        {
            return DBContext.SingleOrDefault<TPassType>(sql);
        }

        public IEnumerable<TPassType> Query<TPassType>()
        {
            var pd = Database.PocoData.ForType(typeof(TPassType));
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return DBContext.Query<TPassType>(sql);
        }

        public IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args)
        {
            return DBContext.Query<TPassType>(sql, args);
        }

        public IEnumerable<TPassType> Query<TPassType>(string sql)
        {
            return DBContext.Query<TPassType>(sql);
        }

        public List<TPassType> Fetch<TPassType>()
        {

            var pd = Database.PocoData.ForType(typeof(TPassType));
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return DBContext.Fetch<TPassType>(sql);
        }

        public List<TPassType> Fetch<TPassType>(string sql, params object[] args)
        {
            return DBContext.Fetch<TPassType>(sql, args);
        }

        public List<TPassType> Fetch<TPassType>(string sql)
        {
            return DBContext.Fetch<TPassType>(sql);
        }

        public int Insert(object poco)
        {
            return Convert.ToInt32(DBContext.Insert(poco));
        }

        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return Convert.ToInt32(DBContext.Insert(tableName, primaryKeyName, autoIncrement, poco));
        }

        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return Convert.ToInt32(DBContext.Insert(tableName, primaryKeyName, poco));
        }

        public int Update(object poco)
        {
            return DBContext.Update(poco);
        }

        public int Update(object poco, object primaryKeyValue)
        {
            return DBContext.Update(poco, primaryKeyValue);
        }

        public int Update(string tableName, string primaryKeyName, object poco)
        {
            return DBContext.Update(tableName, primaryKeyName, poco);
        }

        public int Update(object poco, IEnumerable<string> columns)
        {
            return DBContext.Update(poco, columns);
        }

        public int Update<TPassType>(string sql)
        {
            return DBContext.Update<TPassType>(sql);
        }

        public int Update<TPassType>(string sql, params object[] args)
        {
            return DBContext.Update<TPassType>(sql, args);
        }

        public int Delete<TPassType>(object pocoOrPrimaryKey)
        {
            return DBContext.Delete<TPassType>(pocoOrPrimaryKey);
        }

        public int Delete<TPassType>(string sql)
        {
            return DBContext.Delete<TPassType>(sql);
        }

        public int Delete<TPassType>(string sql, params object[] args)
        {
            return DBContext.Delete<TPassType>(sql, args);
        }

        public List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql)
        {
            Page<TPassType> pagedList = DBContext.Page<TPassType>(pageNumber, itemsPerPage, sql) as Page<TPassType>;
            totalItems = pagedList.TotalItems;
            totalPages = pagedList.TotalPages;
            return pagedList.Items;
        }

        public List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql, params object[] args)
        {
            Page<TPassType> pagedList = DBContext.Page<TPassType>(pageNumber, itemsPerPage, sql, args) as Page<TPassType>;
            totalItems = pagedList.TotalItems;
            totalPages = pagedList.TotalPages;
            return pagedList.Items;
        }

        public long GetCounts<TPassType>(string sql, params object[] args)
        {
            var pd = Database.PocoData.ForType(typeof(TPassType));
            var strsql = "SELECT count(1) FROM " + pd.TableInfo.TableName;         
            return DBContext.ExecuteScalar<long>(strsql, args);
        }

        public long GetCounts<TPassType>(string sql)
        {
            return GetCounts<TPassType>(sql,new object[]{});
        }

        public bool Exists<TPassType>(string sql, params object[] args)
        {
            return (GetCounts<TPassType>(sql, args)>0);
        }

        public bool Exists<TPassType>(string sql)
        {
            return (GetCounts<TPassType>(sql) > 0);
        }


        public string LastSQL()
        {
            return DBContext.LastSQL;
        }
    }
}

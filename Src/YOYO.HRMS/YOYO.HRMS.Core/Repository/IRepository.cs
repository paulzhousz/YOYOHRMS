using System.Collections.Generic;

namespace YOYO.HRMS.Core.Repository
{
    public interface IRepository<T>
    {
        TPassType Single<TPassType>(object primaryKey);
        TPassType Single<TPassType>(string sql, params object[] args);
        TPassType Single<TPassType>(string sql);

        TPassType SingleOrDefault<TPassType>(object primaryKey);
        TPassType SingleOrDefault<TPassType>(string sql, params object[] args);
        TPassType SingleOrDefault<TPassType>(string sql);

        IEnumerable<TPassType> Query<TPassType>();
        IEnumerable<TPassType> Query<TPassType>(string sql);
        IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args);
        List<TPassType> Fetch<TPassType>();
        List<TPassType> Fetch<TPassType>(string sql);
        List<TPassType> Fetch<TPassType>(string sql, params object[] args);
        int Insert(object poco);
        int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco);
        int Insert(string tableName, string primaryKeyName, object poco);
        int Update(object poco);
        int Update(object poco, object primaryKeyValue);
        int Update(string tableName, string primaryKeyName, object poco);
        int Update(object poco, IEnumerable<string> columns);
        int Update<TPassType>(string sql);
        int Update<TPassType>(string sql, params object[] args);
        int Delete<TPassType>(object pocoOrPrimaryKey);
        int Delete<TPassType>(string sql);
        int Delete<TPassType>(string sql, params object[] args);
        List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql);
        List<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, out long totalItems, out long totalPages, string sql, params object[] args);
        long GetCounts<TPassType>(string sql, params object[] args);
        long GetCounts<TPassType>(string sql);
        bool Exists<TPassType>(string sql, params object[] args);
        bool Exists<TPassType>(string sql);
        string LastSQL();
    }
}

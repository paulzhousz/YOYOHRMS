using System.Collections.Generic;


namespace YOYO.HRMS.BusinessLogic
{
    public interface IBaseService<T>
    {
        IEnumerable<TPassType> Query<TPassType>();
        IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args);
        IEnumerable<TPassType> Query<TPassType>(string sql);

        TPassType GetEntity<TPassType>(object primaryKey);
        TPassType GetEntity<TPassType>(string sql, params object[] args);
        TPassType GetEntity<TPassType>(string sql);

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

    }
}
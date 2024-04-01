using PhotoCloud.DatabaseClient;

namespace PhotoCloud.DataService
{
    public class BaseService<T> : IBaseService<T>
    {
        private IDatabaseClient databaseClient;

        public BaseService(SQLServerClient sqlServerClient)
        {
            databaseClient = sqlServerClient;
        }

        public virtual bool Add(T t)
        {
            var sql = SQLBuilder.BuildInsertSQL<T>();
            return databaseClient.Execute(sql, t);
        }

        public virtual int AddAndReturnAutoID(T t)
        {
            var sql = SQLBuilder.BuildInsertSQL<T>(true);
            return databaseClient.ExecuteScalar<int>(sql, t);
        }

        public virtual bool Delete(int id)
        {
            var sql = SQLBuilder.BuildDeleteSQL<T>();
            return databaseClient.Execute(sql, new { ID = id });
        }

        public virtual bool Delete(IList<int> ids)
        {
            try
            {
                databaseClient.UseTransaction();
                foreach (var id in ids)
                {
                    Delete(id);
                }
                databaseClient.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                databaseClient.RollbackTransaction();
                throw new Exception("Database batch deletion failed.", ex);
            }
        }

        public virtual bool Update(T t)
        {
            var sql = SQLBuilder.BuildUpdateSQL<T>();
            return databaseClient.Execute(sql, t);
        }

        public virtual T? Get(int id)
        {
            var sql = SQLBuilder.BuildSelectItemSQL<T>();
            return databaseClient.QueryFirstOrDefault<T>(sql, new { ID = id });
        }

        public virtual IEnumerable<T> GetList(bool isDESC = false)
        {
            var sql = SQLBuilder.BuildSelectListSQL<T>(isDESC);
            return databaseClient.Query<T>(sql);
        }

        public virtual IEnumerable<T> GetList(out int count, int pageIndex = 1, int pageSize = 50)
        {
            string countSql;
            var sql = SQLBuilder.BuildSelectSQLByPage<T>(out countSql);
            sql = sql.Replace("@PerPage", pageSize.ToString()).Replace("@LastCount", ((pageIndex - 1) * pageSize).ToString());
            count = databaseClient.ExecuteScalar<int>(countSql);
            return databaseClient.Query<T>(sql);
        }
    }
}

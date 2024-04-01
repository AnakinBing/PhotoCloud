using System.Data;

namespace PhotoCloud.DatabaseClient
{
    public interface IDatabaseClient : IDisposable
    {
        bool Execute(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);

        T? ExecuteScalar<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);

        T? QueryFirstOrDefault<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<T> Query<T>(string sql, object? param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        void UseTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}

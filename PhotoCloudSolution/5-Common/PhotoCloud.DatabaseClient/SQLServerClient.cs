using Dapper;
using Microsoft.Extensions.Options;
using PhotoCloud.DatabaseClient.Config;
using System.Data;
using System.Data.SqlClient;

namespace PhotoCloud.DatabaseClient
{
    public class SQLServerClient : IDatabaseClient
    {
        private readonly DatabaseSettings _settings;
        private IDbConnection? dbConnection = null;
        private IDbTransaction? dbTransaction = null;

        public SQLServerClient(IOptions<DatabaseSettings> options)
        {
            this._settings = options.Value;
        }

        public bool Execute(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = this.GetDbConnection().Execute(sql, param, this.dbTransaction, commandTimeout, commandType);
            return result > 0;
        }

        public T? ExecuteScalar<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.GetDbConnection().ExecuteScalar<T>(sql, param, this.dbTransaction, commandTimeout, commandType);
        }

        public T? QueryFirstOrDefault<T>(string sql, object? param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.GetDbConnection().QueryFirstOrDefault<T>(sql, param, this.dbTransaction, commandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object? param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.GetDbConnection().Query<T>(sql, param, this.dbTransaction, buffered, commandTimeout, commandType);
        }

        public void UseTransaction()
        {
            if (this.dbTransaction == null)
            {
                this.dbTransaction = this.GetDbConnection().BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            this.dbTransaction?.Commit();
            this.dbTransaction?.Dispose();
            this.dbTransaction = null;
        }

        public void RollbackTransaction()
        {
            this.dbTransaction?.Rollback();
            this.dbTransaction?.Dispose();
            this.dbTransaction = null;
        }

        public void Dispose()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Dispose();
                dbTransaction = null;
            }

            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
                dbConnection = null;
            }
        }

        #region Private Functions

        private IDbConnection GetDbConnection()
        {
            if (this.dbConnection == null)
            {
                this.dbConnection = new SqlConnection(this._settings.ConnectionString);
            }
            if (this.dbConnection.State == ConnectionState.Closed)
            {
                this.dbConnection.Open();
            }
            return this.dbConnection;
        }

        #endregion
    }
}

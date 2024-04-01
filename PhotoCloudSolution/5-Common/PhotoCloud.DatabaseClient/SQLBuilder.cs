using PhotoCloud.DatabaseClient.Attributes;
using PhotoCloud.DatabaseClient.Model;
using System.Reflection;
using System.Text;

namespace PhotoCloud.DatabaseClient
{
    /// <summary>
    /// Build CRUD SQL statements.
    /// </summary>
    public class SQLBuilder
    {
        /// <summary>
        /// Build Insert SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isReturnAutoID">Whether to return the auto-incrementing ID</param>
        /// <returns></returns>
        public static string BuildInsertSQL<T>(bool isReturnAutoID = false)
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("INSERT INTO {0}(", table.Name));
            for (int i = 0; i < table.Columns?.Count; i++)
            {
                if (i < table.Columns?.Count - 1)
                    sql.Append(table.Columns?[i] + ",");
                else
                    sql.Append(table.Columns?[i]);
            }
            if (isReturnAutoID)
                sql.Append(string.Format(")OUTPUT INSERTED.{0} VALUES(", table.PrimaryKey));
            else
                sql.Append(")VALUES(");
            for (int i = 0; i < table.Columns?.Count; i++)
            {
                if (i < table.Columns?.Count - 1)
                    sql.Append("@" + table.Columns?[i] + ",");
                else
                    sql.Append("@" + table.Columns?[i]);
            }
            sql.Append(")");
            return sql.ToString();
        }

        /// <summary>
        /// Build Delete SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string BuildDeleteSQL<T>()
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("DELETE {0} from {0} WHERE {1}=@{1}", table.Name, table.PrimaryKey));
            return sql.ToString();
        }

        /// <summary>
        /// Build Update SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string BuildUpdateSQL<T>()
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("UPDATE {0} set ", table.Name));
            for (int i = 0; i < table.Columns?.Count; i++)
            {
                if (i < table.Columns?.Count - 1)
                    sql.Append(string.Format("{0}=@{0},", table.Columns?[i]));
                else
                    sql.Append(string.Format("{0}=@{0}", table.Columns?[i]));
            }
            sql.Append(string.Format(" WHERE {0}=@{0}", table.PrimaryKey));
            return sql.ToString();
        }

        /// <summary>
        /// Build Select Item SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string BuildSelectItemSQL<T>()
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("SELECT * FROM {0} WHERE {1}=@{1}", table.Name, table.PrimaryKey));
            return sql.ToString();
        }

        /// <summary>
        /// Build Select List SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isDESC"></param>
        /// <returns></returns>
        public static string BuildSelectListSQL<T>(bool isDESC = false)
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("SELECT * FROM {0}", table.Name));
            if (isDESC)
                sql.Append(string.Format(" ORDER  BY {0} DESC", table.PrimaryKey));
            else
                sql.Append(string.Format(" ORDER  BY {0} ASC", table.PrimaryKey));
            return sql.ToString();
        }

        /// <summary>
        /// Build Select List By Page SQL statements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="countSql"></param>
        /// <returns></returns>
        public static string BuildSelectSQLByPage<T>(out string countSql)
        {
            StringBuilder sql = new StringBuilder();
            TableModel table = GetTable<T>();
            sql.Append(string.Format("SELECT TOP @PerPage * FROM {0}", table.Name));
            sql.Append(string.Format(" WHERE {0} NOT IN(SELECT TOP @LastCount {0} FROM {1})", table.PrimaryKey, table.Name));
            countSql = string.Format("SELECT COUNT({0}) FROM {1}", table.PrimaryKey, table.Name);
            return sql.ToString();
        }

        private static TableModel GetTable<T>()
        {
            TableModel table = new TableModel();
            Type modelType = typeof(T);
            TableAttribute tableAttribute = modelType.GetCustomAttributes<TableAttribute>().First();
            table.Name = KeywordPack(tableAttribute.Table);

            List<string> columns = new List<string>();
            foreach (PropertyInfo pInfo in modelType.GetProperties())
            {
                PrimaryKeyAttribute? primaryKeyAttribute = pInfo.GetCustomAttribute<PrimaryKeyAttribute>();
                if (primaryKeyAttribute != null)
                {
                    table.PrimaryKey = KeywordPack(primaryKeyAttribute.PrimaryKey);
                }
                ColumnAttribute? columnAttribute = pInfo.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    columns.Add(KeywordPack(columnAttribute.Column));
                }
            }
            table.Columns = columns;
            return table;
        }

        private static string KeywordPack(string keyword)
        {
            string[] keyArr = new string[] { "ADD", "ALL", "ALTER", "AND", "ANY", "AS", "USER" };
            if (keyArr.Contains(keyword.ToUpper()))
                keyword = "[" + keyword + "]";
            return keyword;
        }
    }
}

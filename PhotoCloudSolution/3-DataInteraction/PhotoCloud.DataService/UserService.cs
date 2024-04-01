using PhotoCloud.DatabaseClient;
using PhotoCloud.Model.Database;

namespace PhotoCloud.DataService
{
    public class UserService : BaseService<User>, IUserService
    {
        private IDatabaseClient _databaseClient;

        public UserService(SQLServerClient sqlServerClient) : base(sqlServerClient)
        {
            _databaseClient = sqlServerClient;
        }

        public User? Get(string email, string password)
        {
            string sql = "SELECT * FROM [User] WHERE Email=@email AND Password=@password";
            return _databaseClient.QueryFirstOrDefault<User>(sql, new { email, password });
        }
    }
}

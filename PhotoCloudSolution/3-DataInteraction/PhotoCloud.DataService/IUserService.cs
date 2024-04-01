using PhotoCloud.Model.Database;

namespace PhotoCloud.DataService
{
    public interface IUserService : IBaseService<User>
    {
        User? Get(string email, string password);
    }
}

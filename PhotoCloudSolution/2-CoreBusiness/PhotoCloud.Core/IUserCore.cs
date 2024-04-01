using PhotoCloud.Model.Core.Request;
using PhotoCloud.Model.Core.Response;

namespace PhotoCloud.Core
{
    public interface IUserCore
    {
        LoginResponse Login(LoginRequest request);
    }
}

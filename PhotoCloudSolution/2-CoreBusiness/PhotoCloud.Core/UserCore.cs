using PhotoCloud.DataService;
using PhotoCloud.Model.Core.Request;
using PhotoCloud.Model.Core.Response;
using PhotoCloud.Model.Database;
using PhotoCloud.Utility;

namespace PhotoCloud.Core
{
    public class UserCore : IUserCore
    {
        private readonly IUserService userService;

        public UserCore(IUserService userService)
        {
            this.userService = userService;
        }

        public LoginResponse Login(LoginRequest request)
        {
            User? user = userService.Get(request.Email, Util.EncryptionMD5(request.Password));
            if (user == null)
            {
                throw new Exception("Email or password is incorrect.");
            }
            return new LoginResponse
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                CreationTime = user.CreationTime
            };
        }
    }
}

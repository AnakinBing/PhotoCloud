using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhotoCloud.Core;
using PhotoCloud.Model.Core;
using PhotoCloud.Model.Core.Request;
using PhotoCloud.Model.Core.Response;
using PhotoCloud.WebAPI.Configure;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PhotoCloud.WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private IUserCore userCore;

        public UserController(IUserCore userCore)
        {
            this.userCore = userCore;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultModel Login(LoginRequest request)
        {
            LoginResponse response = userCore.Login(request);

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,response.Name),
                new Claim("Id",response.ID.ToString()),
                new Claim("UserName",response.Name),
                new Claim("Email",response.Email),
                new Claim("Role","admin")
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigureToken.GetSecurityKey()));
            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            response.Token = jwtToken;

            return new ResultModel
            {
                Code = (int)HttpStatusCode.OK,
                Data = response,
                Message = $"Login successful."
            };
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoCloud.Model.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhotoCloud.WebAPI.Controllers
{
    /// <summary>
    /// Base class for all APIs.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public AuthInfo ValidateToken()
        {
            string? jwtToken = HttpContext.Request.Headers["Authorization"];
            jwtToken = jwtToken?.Split(" ")[1];

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(jwtToken);
            IEnumerable<Claim> claims = jwt.Claims;

            return new AuthInfo()
            {
                ID = Convert.ToInt32(claims.First(x => x.Type == "Id").Value),
                UserName = claims.First(x => x.Type == "UserName").Value,
                Email = claims.First(x => x.Type == "Email").Value,
                Role = Convert.ToInt32(claims.First(x => x.Type == "Role").Value),
                Expiration = Convert.ToInt64(claims.First(x => x.Type == "exp").Value)
            };
        }
    }
}

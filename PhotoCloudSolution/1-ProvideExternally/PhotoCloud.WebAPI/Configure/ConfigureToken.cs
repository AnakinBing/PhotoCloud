using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PhotoCloud.WebAPI.Configure
{
    public static class ConfigureToken
    {
        private static string? SecurityKey = null;

        public static string GetSecurityKey()
        {
            if (SecurityKey == null)
                SecurityKey = Guid.NewGuid().ToString();
            return SecurityKey;
        }

        public static void ConfigureJWTToken(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecurityKey())),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    };
                });
        }
    }
}

using Microsoft.OpenApi.Models;

namespace PhotoCloud.WebAPI.Configure
{
    public static class ConfigureSwagger
    {
        public static void ConfigureSwaggerUp(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "PhotoCloud.WebAPI",
                    Version = "v1",
                    Description = "PhotoCloud.WebAPI"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

                c.OrderActionsBy(o => o.RelativePath);
            });
        }
    }
}

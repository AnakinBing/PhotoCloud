namespace PhotoCloud.WebAPI.Configure
{
    public static class ConfigureCrossDomain
    {
        public static void ConfigureCrossDomainUp(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}

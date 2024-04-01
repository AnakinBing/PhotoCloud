using Microsoft.Extensions.FileProviders;
using PhotoCloud.Utility;
using PhotoCloud.WebAPI.Configure;
using PhotoCloud.WebAPI.Filter;

namespace PhotoCloud.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.StartupPath = AppContext.BaseDirectory;

            // Configure Swagger
            services.ConfigureSwaggerUp();

            // Configure Cross Domain
            services.ConfigureCrossDomainUp();

            // Configure Services
            services.ConfigureServicesUp();

            // Configure JWT Token
            services.ConfigureJWTToken();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", "PhotoCloud.WebAPI V1");
                c.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "photo")),
                RequestPath = "/photo"
            });
        }
    }
}

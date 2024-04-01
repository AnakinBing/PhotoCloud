using PhotoCloud.Core;
using PhotoCloud.DatabaseClient;
using PhotoCloud.DatabaseClient.Config;
using PhotoCloud.DataService;

namespace PhotoCloud.WebAPI.Configure
{
    public static class ConfigureServices
    {
        public static void ConfigureServicesUp(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddOptions<DatabaseSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("DatabaseSettings").Bind(settings);
            });

            services.AddScoped<SQLServerClient, SQLServerClient>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserCore, UserCore>();


            //services.AddOptions<EmailServiceSettings>()
            //.Configure<IConfiguration>((settings, configuration) =>
            //{
            //    configuration.GetSection("EmailServiceSettings").Bind(settings);
            //});


            //services.AddScoped<MySQLClient, MySQLClient>();

            //services.AddScoped<IAzureDevOpsUserStoryService, AzureDevOpsUserStoryService>();
            //services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<IReportService, ReportService>();
            //services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<ITeamMemberService, TeamMemberService>();
            //services.AddScoped<ITeamService, TeamService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ITaskService, TaskService>();
            //services.AddScoped<ITaskDetailService, TaskDetailService>();
            //services.AddScoped<IAzureDevOpsSettingService, AzureDevOpsSettingService>();

            //services.AddScoped<IEmailCore, EmailCore>();
            //services.AddScoped<IProjectCore, ProjectCore>();
            //services.AddScoped<IReportCore, ReportCore>();
            //services.AddScoped<ITeamCore, TeamCore>();
            //services.AddScoped<IUserCore, UserCore>();
            //services.AddScoped<ITaskCore, TaskCore>();
            //services.AddScoped<IAzureDevOpsCore, AzureDevOpsCore>();
        }
    }
}

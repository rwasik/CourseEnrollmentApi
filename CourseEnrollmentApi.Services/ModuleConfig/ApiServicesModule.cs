using CourseEnrollmentApi.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseEnrollmentApi.Services.ModuleConfig
{
    public static class ApiServicesModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
        }
    }
}
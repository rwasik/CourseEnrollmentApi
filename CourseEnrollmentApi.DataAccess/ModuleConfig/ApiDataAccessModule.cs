using CourseEnrollmentApi.DataAccess.Context;
using CourseEnrollmentApi.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CourseEnrollmentApi.DataAccess.ModuleConfig
{
    public static class ApiDataAccessModule
    {
        public static void ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(ServiceLifetime.Scoped);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}

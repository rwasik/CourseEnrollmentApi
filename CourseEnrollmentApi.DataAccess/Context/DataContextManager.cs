using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseEnrollmentApi.DataAccess.Context
{
    public static class DataContextManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                {
                    try
                    {
                        dataContext.Database.Migrate();
                    }
                    catch
                    {
                        // log exception
                        throw;
                    }
                }
            }

            return host;
        }
    }
}

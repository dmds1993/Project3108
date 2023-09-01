using Domain.Infra.SqlServer.Repository;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infra.SqlServer.DependecyInjection
{
    public static class SQLServeExtension
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TASK_CONTROL_DATA_BASE");
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPBIRepository, PBIRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}

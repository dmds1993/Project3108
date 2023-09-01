using Domain.Interfaces.Service;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyInjection
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services
                .AddPBIService()
                .AddTaskService();
        }

        private static IServiceCollection AddPBIService(this IServiceCollection services)
        {
            services.AddScoped<IPBIService, PBIService>();
            return services;
        }

        private static IServiceCollection AddTaskService(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}

using ITravel.Data.IRepositories;
using ITravel.Data.Repositories;
using ITravel.Service.Interfaces;
using ITravel.Service.Services;

namespace ITravel.Configurations.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IPaymeService, PaymeService>();
            
        }
    }
}

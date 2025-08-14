using ITravel.Data.Contexts;
using ITravel.Data.IRepositories;
using ITravel.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITravel.Configurations.LayerConfigurations
{
    public static class DataAccessConfiguration
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DatabaseConnection")!;
            services.AddDbContext<TourismDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 11))));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

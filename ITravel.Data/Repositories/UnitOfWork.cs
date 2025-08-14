using ITravel.Data.Contexts;
using ITravel.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TourismDbContext dbContext;
        public ISettingRepository SoftSetting { get; }
        public IPaymeOrderRepository PaymeOrderRepository { get; }
        public IPaymeTransRepository PaymeTransRepository { get; }

        public UnitOfWork(TourismDbContext appDbContext)
        {
            this.dbContext = appDbContext;
            PaymeOrderRepository = new PaymeOrderRepository(appDbContext);
            PaymeTransRepository = new PaymeTransRepository(appDbContext);
            SoftSetting = new SettingRepository(appDbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

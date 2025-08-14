using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.IRepositories
{
    public interface IUnitOfWork
    {
        public ISettingRepository SoftSetting { get; }
        public IPaymeOrderRepository PaymeOrderRepository { get; }
        public IPaymeTransRepository PaymeTransRepository { get; }

        public Task<int> SaveChangesAsync();
    }
}

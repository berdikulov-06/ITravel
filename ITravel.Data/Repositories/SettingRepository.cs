using ITravel.Data.Contexts;
using ITravel.Data.IRepositories;
using ITravel.Domain.Entities.SoftSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.Repositories
{
    public class SettingRepository : BaseRepository<SoftSettings>, ISettingRepository
    {
        public SettingRepository(TourismDbContext context) : base(context)
        {
        }
    }
}

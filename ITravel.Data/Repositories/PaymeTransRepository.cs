using ITravel.Data.Contexts;
using ITravel.Data.IRepositories;
using ITravel.Domain.Entities.Payme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.Repositories
{
    public class PaymeTransRepository : BaseRepository<payme_transaction>, IPaymeTransRepository
    {
        public PaymeTransRepository(TourismDbContext context) : base(context)
        {
        }
    }
}

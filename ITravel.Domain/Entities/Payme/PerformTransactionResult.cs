using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class PerformTransactionResult
    {
        public string? transaction { get; set; }
        public long? perform_time { get; set; }
        public int? state { get; set; }
    }
}

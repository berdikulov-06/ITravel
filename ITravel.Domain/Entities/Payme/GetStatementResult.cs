using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class GetStatementResult
    {
        public string id { get; set; }
        public DateTime? time { get; set; }
        public int? amount { get; set; }
        public Account account { get; set; }
        public DateTime? create_time { get; set; }
        public DateTime? perform_time { get; set; }
        public DateTime? cancel_time { get; set; }
        public long? transaction { get; set; }
        public int? state { get; set; }
        public int? reason { get; set; }
    }
}

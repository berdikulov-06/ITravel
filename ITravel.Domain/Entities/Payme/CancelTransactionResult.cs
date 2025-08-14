using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class CancelTransactionResult
    {
        public string? transaction { get; set; }
        public long? cancel_time { get; set; }
        public int? state { get; set; }

        public CancelTransactionResult(string? transaction = null, long? cancel_time = null, int? state = null)
        {
            this.transaction = transaction;
            this.cancel_time = cancel_time;
            this.state = state;
        }
    }
}

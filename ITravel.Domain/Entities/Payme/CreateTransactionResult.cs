using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class CreateTransactionResult
    {
        public long create_time { get; set; }

        public string transaction { get; set; }

        public int state { get; set; }
    }
}

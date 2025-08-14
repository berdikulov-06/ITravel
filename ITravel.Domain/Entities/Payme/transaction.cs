using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class transaction
    {
        public int id { get; set; }
        public int user_id { get; set; }

        public string source { get; set; }
        public int source_id { get; set; }

        public int amount { get; set; }

        public string type { get; set; }

        public string details { get; set; }

        public long create_time { get; set; }
    }
}

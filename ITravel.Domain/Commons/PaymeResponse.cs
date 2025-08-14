using ITravel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Commons
{
    public class PaymeResponse<T>
    {
        public T result { get; set; }

        public int id { get; set; }

        public PaymeError error { get; set; }
    }
}

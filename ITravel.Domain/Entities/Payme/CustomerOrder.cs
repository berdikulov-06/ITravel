using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class CustomerOrder
    {
        [Key]
        public long id { get; set; }

        public int amount { get; set; }

        public bool delivered { get; set; }
    }
}

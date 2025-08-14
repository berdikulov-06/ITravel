using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? user_id { get; set; }

        public string? source { get; set; }

        public int? source_id { get; set; }

        public decimal amount { get; set; }

        public string? status { get; set; }

        public DateTime? create_time { get; set; }

        public string? phone { get; set; }
    }
}

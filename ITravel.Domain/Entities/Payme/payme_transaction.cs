using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ITravel.Domain.Enums;

namespace ITravel.Domain.Entities.Payme
{
    public class payme_transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long? orderid { get; set; }

        public string paycomId { get; set; }

        public decimal amount { get; set; }

        public long paycomTime { get; set; }

        public long createTime { get; set; }

        public long performTime { get; set; }

        public long cancelTime { get; set; }

        public OrderCancelReason? reason { get; set; }

        public TransactionState state { get; set; }
    }
}

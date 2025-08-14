using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Enums
{
    public class TransactionService
    {
        public const string SOURCE_PAYME = "payme";

        public const string TYPE_TOP_UP = "top-up";
        public const string TYPE_BONUS = "bonus";
        public const string TYPE_EXPENSE = "expense";
        public const string TYPE_REFUND = "refund";

        public const string SOURCE_ORDER = "order";

        public const string ORDER_NEW = "new";
        public const string ORDER_PENDING = "pending";
        public const string ORDER_PAID = "paid";
    }
}

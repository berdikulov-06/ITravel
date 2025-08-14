using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Enums
{
    public enum OrderCancelReason
    {
        RECEIVER_NOT_FOUND = 1,
        DEBIT_OPERATION_ERROR = 2,
        TRANSACTION_ERROR = 3,
        TRANSACTION_TIMEOUT = 4,
        MONEY_BACK = 5,
        UNKNOWN_ERROR = 10
    }
}

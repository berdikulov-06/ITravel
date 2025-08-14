using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Enums
{
    public class MerchantOptions
    {
        public const int ERROR_INTERNAL_SYSTEM = -32400;
        public const int ERROR_INSUFFICIENT_PRIVILEGE = -32504;
        public const int ERROR_INVALID_JSON_RPC_OBJECT = -32600;
        public const int ERROR_METHOD_NOT_FOUND = -32601;
        public const int ERROR_INVALID_AMOUNT = -31001;
        public const int ERROR_TRANSACTION_NOT_FOUND = -31003;
        public const int ERROR_INVALID_ACCOUNT = -31050;
        public const int ERROR_COULD_NOT_CANCEL = -31007;
        public const int ERROR_COULD_NOT_PERFORM = -31008;

        public const int STATE_CREATED = 1;
        public const int STATE_COMPLETED = 2;
        public const int STATE_CANCELLED = -1;
        public const int STATE_CANCELLED_AFTER_COMPLETE = -2;

        public const int REASON_RECEIVERS_NOT_FOUND = 1;
        public const int REASON_PROCESSING_EXECUTION_FAILED = 2;
        public const int REASON_EXECUTION_FAILED = 3;
        public const int REASON_CANCELLED_BY_TIMEOUT = 4;
        public const int REASON_FUND_RETURNED = 5;
        public const int REASON_UNKNOWN = 10;

        public const int TIMEOUT = 43200000;
    }
}

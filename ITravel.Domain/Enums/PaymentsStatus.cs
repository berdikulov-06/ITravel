using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Enums
{
    public class PaymentsStatus
    {
        public const string INPUT = "input";
        public const string WAITING = "waiting";
        public const string PREAUTH = "preauth";
        public const string CONFIRMED = "confirmed";
        public const string REJECTED = "rejected";
        public const string REFUNDED = "refunded";
        public const string ERROR = "error";
    }
}

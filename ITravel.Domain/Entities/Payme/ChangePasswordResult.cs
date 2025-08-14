using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.Payme
{
    public class ChangePasswordResult
    {
        public bool success { get; set; }

        public ChangePasswordResult(bool success)
        {
            this.success = success;
        }
    }
}

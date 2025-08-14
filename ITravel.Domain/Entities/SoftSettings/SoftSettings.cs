using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Domain.Entities.SoftSettings
{
    public class SoftSettings
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string PaymeKassId { get; set; }
        public string PaymeKassPassw { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}

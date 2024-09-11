using SednaReservationAPI.Domain.Entities.Common;
using SednaReservationAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Domain.Entities
{
    public class Endpoint: BaseEntitity
    {
        public Endpoint() { 
        Roles = new HashSet<AppRole>();
        }
        public string ActionType { get; set; }
        public  string HttpType { get; set; }
        public string Definiation { get; set; }
        public  string Code { get; set; }
        public Menu Menu { get; set; }
        public ICollection<AppRole> Roles { get; set; }
    }
}

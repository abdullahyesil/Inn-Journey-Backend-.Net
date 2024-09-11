using SednaReservationAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Domain.Entities
{
    public class Menu : BaseEntitity
    {
        public string Name { get; set; }
     public ICollection<Endpoint> Endpoints { get; set; }
    }
}

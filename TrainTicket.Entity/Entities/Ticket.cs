using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicket.Entity.Entities
{
    public  class Ticket
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FromWhere { get; set; }
        public string ToWhere { get; set; }
        public int seatNo { get; set; }
        public int Price { get; set; }
        public int TrainRouteId { get; set; }       //Bir biletin yalnızca bir rotası olabilir.
        public TrainRoute  trainRoute { get; set; } 

    }
}

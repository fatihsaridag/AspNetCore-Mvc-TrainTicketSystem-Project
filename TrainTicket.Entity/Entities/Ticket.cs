using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicket.Entity.Entities
{
    public  class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FromWhere { get; set; }
        public string ToWhere { get; set; }
        public int seatNo { get; set; }
        public int Price { get; set; }
        public int TrainRouteId { get; set; }       //Bir biletin yalnızca bir rotası olabilir.
        public TrainRoute  trainRoute { get; set; } 

    }
}

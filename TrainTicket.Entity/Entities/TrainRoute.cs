using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicket.Entity.Entities
{
    public class TrainRoute
    {
        public int RouteId { get; set; }
        public string StartRo { get; set; }
        public string Ro1 { get; set; }
        public string Ro2 { get; set; }
        public string Ro3 { get; set; }
        public string FinishRo { get; set; }
        public string Time { get; set; }
        public string Clock { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }

        public List<Ticket> Tickets { get; set; }   // Bir rotanın birden fazla bileti olabilir.


    }
}

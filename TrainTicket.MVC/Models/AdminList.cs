using System.Collections.Generic;
using TrainTicket.Entity.Entities;

namespace TrainTicket.MVC.Models
{
    public class AdminList
    {
        public List<Ticket> Tickets { get; set; }
        public List<TrainRoute> TrainRoutes{ get; set; }
        public List<City> Cities { get; set; }
    }
}

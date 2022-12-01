using System.Collections.Generic;
using TrainTicket.Entity.Entities;

namespace TrainTicket.MVC.Models
{
    public class TicketRouteViewModel
    {
        public List<City> Cities{ get; set; }
        public List<TrainRoute> TrainRoutes{ get; set; }

        public TrainRoute WhereToTrainRoute { get; set; }

        public RouteModel  RouteModel { get; set; } 
    }
}

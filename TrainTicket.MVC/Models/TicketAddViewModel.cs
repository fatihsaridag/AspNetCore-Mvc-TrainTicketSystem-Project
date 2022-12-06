using TrainTicket.Entity.Entities;

namespace TrainTicket.MVC.Models
{
    public class TicketAddViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FromWhere { get; set; }
        public string ToWhere { get; set; }
        public int TrainRouteId { get; set; }      
        public TrainRoute trainRoute { get; set; }
    }
}

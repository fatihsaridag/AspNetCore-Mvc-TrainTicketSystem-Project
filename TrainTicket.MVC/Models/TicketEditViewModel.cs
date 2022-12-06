namespace TrainTicket.MVC.Models
{
    public class TicketEditViewModel
    {
        public  int TicketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FromWhere { get; set; }
        public string ToWhere { get; set; }
    }
}

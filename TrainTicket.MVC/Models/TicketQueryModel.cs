using System.ComponentModel.DataAnnotations;

namespace TrainTicket.MVC.Models
{
    public class TicketQueryModel
    {
        [Display(Name="Bilet Numaranız")]
        [Required]
        public string TicketNo { get; set; }
    }
}

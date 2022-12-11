using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainTicket.MVC.Models
{
    public class TicketEditViewModel
    {

        public  int TicketId { get; set; }
        [Display(Name = "Adınız")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Soyadınız")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Email Adresiniz")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Telefon Numaranız")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Nereden")]
        [Required]
        public string FromWhere { get; set; }
        [Display(Name = "Nereye")]
        [Required]
        public string ToWhere { get; set; }
    }
}

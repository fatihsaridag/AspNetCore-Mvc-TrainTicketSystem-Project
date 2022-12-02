using System.ComponentModel.DataAnnotations;
using TrainTicket.Entity.Entities;

namespace TrainTicket.MVC.Models
{
    public class TicketBuyViewModel
    {
        [Display(Name ="Adınız")]
        [Required(ErrorMessage ="Lütfen adınızı giriniz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string LastName { get; set; }

        [Display(Name = "Telefon Numaranız")]
        [Required(ErrorMessage = "Lütfen telefonunuzu giriniz")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Adresiniz")]
        [Required(ErrorMessage = "Lütfen emailinizi giriniz")]
        [EmailAddress]
        public string Email { get; set; }


        public TrainRoute TrainRoute { get; set; }

    }
}

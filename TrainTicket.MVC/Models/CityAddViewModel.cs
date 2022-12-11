using System.ComponentModel.DataAnnotations;

namespace TrainTicket.MVC.Models
{
    public class CityAddViewModel
    {
        [Display(Name ="Şehir Adı")]
        [Required]
        public string CityName { get; set; }
    }
}

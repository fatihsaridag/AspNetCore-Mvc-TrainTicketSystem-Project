using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainTicket.MVC.Models
{
    public class CityEditViewModel
    {
        public int CityId { get; set; }

        [Display(Name = "Şehir Adı")]
        [Required]
        public string CityName { get; set; }
    }
}

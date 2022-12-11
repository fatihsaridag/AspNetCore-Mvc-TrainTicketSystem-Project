using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainTicket.MVC.Models
{
    public class TrainRouteEditViewModel
    {
        public int RouteId { get; set; }
        [Display(Name = "Başlangıç Rotası")]
        [Required]
        public string StartRo { get; set; }
        [Display(Name = "Rota 1")]
        [Required]
        public string Ro1 { get; set; }
        [Display(Name = "Rota 2")]
        [Required]
        public string Ro2 { get; set; }
        [Display(Name = "Rota 3")]
        [Required]
        public string Ro3 { get; set; }
        [Display(Name = "Bitiş Rotası")]
        [Required]
        public string FinishRo { get; set; }
        [Display(Name = "Hareket Zamanı")]
        [Required]
        public string Time { get; set; }
        [Display(Name = "Hareket Saati")]
        [Required]
        public string Clock { get; set; }
        public string Image { get; set; }

        [Display(Name = "Fiyatı")]
        [Required]
        public int Price { get; set; }

    }
}

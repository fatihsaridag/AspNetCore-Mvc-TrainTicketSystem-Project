using System.ComponentModel.DataAnnotations;

namespace TrainTicket.MVC.Models
{
    public class LoginViewModel
    {
        [Display(Name ="Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

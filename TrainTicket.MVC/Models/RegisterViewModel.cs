using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainTicket.MVC.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Adınız gereklidir")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Adresiniz gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage ="Şifreniz gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage ="Şifre tekrar alanı gereklidir")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler Uyumlu değil")]
        public string ConfirmPassword { get; set; }
    }
}

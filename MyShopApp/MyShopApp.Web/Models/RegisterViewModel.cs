using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MyShopApp.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string? Zip { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }      

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set;}
    }
}

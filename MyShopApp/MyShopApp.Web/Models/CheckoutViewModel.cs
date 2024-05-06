using System.ComponentModel.DataAnnotations;

namespace MyShopApp.Web.Models
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "NumberCart")]
        public int NumberCart  { get; set; }

        [Required]
        [Display(Name = "Validity")]
        public string? Validity { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public int CVV { get; set; }       
    }
}

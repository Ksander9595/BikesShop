using Microsoft.VisualBasic;

namespace MyShopApp.Web.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}

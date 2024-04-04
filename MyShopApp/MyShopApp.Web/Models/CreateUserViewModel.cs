using Microsoft.VisualBasic;

namespace MyShopApp.Web.Models
{
    public class CreateUserViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}


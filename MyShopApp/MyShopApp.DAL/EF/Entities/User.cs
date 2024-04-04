using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser
    {
        //public string? Role { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }       
        public virtual ClientProfile? ClientProfile { get; set; }
        public Order? Order { get; set; }
        
    }
}

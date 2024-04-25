using Microsoft.AspNetCore.Identity;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }       
        public ClientProfile? ClientProfile { get; set; }        
        public Order? Order { get; set; }
        
    }
}

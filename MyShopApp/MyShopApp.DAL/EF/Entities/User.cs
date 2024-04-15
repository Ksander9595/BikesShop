using Microsoft.AspNetCore.Identity;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser<int>
    {
        //public string? Role { get; set; }
        //public int Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }       
        public virtual ClientProfile? ClientProfile { get; set; }
        public Order? Order { get; set; }
        
    }
}

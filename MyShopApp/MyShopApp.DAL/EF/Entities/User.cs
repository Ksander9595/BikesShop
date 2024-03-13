using Microsoft.AspNetCore.Identity;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser
    {
        public string? Role { get; set; }
        public int Year { get; set; }     
        public virtual ClientProfile? ClientProfile { get; set; }
        
    }
}

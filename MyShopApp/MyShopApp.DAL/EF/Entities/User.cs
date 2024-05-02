using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser<int>
    {
        public static ClaimsIdentity Identity { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }       
        public ClientProfile? ClientProfile { get; set; }        
        public Cart? Cart { get; set; }
        
    }
}

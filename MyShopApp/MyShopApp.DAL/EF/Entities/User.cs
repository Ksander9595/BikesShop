using Microsoft.AspNetCore.Identity;

namespace MyShopApp.DAL.EF.Entities
{
    public class User : IdentityUser
    {
        public int Year { get; set; }      
        //public Order Order { get; set; }
    }
}

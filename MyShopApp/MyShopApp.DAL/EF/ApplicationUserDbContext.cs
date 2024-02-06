using MyShopApp.DAL.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace MyShopApp.DAL.EF
{
    public class ApplicationUserDbContext : IdentityDbContext<User>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

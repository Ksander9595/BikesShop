using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.Interfaces
{
    public interface IidentityUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        //ApplicationUserManager UserManager { get; }
        //ApplicationRoleManager RoleManager { get; }
        //ApplicationSignInManager SignInManager { get; }
        IClientManager ClientManager { get; }
        Task SaveAsync();
    }
}

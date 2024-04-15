using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF.Entities;


namespace MyShopApp.DAL.Interfaces
{
    public interface IidentityUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
        SignInManager<User> SignInManager { get; }    

        IClientManager ClientManager { get; }
        Task SaveAsync();
    }
}

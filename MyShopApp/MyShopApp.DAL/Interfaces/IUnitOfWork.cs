using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cart> Carts { get; }
        IRepository<Motorcycle> Motorcycles { get; }
        IRepository<Order> Orders { get; }
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
        SignInManager<User> SignInManager { get; }

        IClientManager ClientManager { get; }
        Task SaveAsync();
    }
}

using MyShopApp.DAL.Identity;

namespace MyShopApp.DAL.Interfaces
{
    public interface IidentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }
        Task SaveAsync();
    }
}

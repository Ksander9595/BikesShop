using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.Interfaces
{
    public interface IClientManager : IDisposable   
    {
        Task CreateAsync(ClientProfile item);
    }
}

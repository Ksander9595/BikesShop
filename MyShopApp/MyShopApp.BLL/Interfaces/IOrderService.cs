using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task MakeOrderAsync(OrderDTO orderDto);        
        Task<IEnumerable<MotorcycleDTO>> GetMotorcycles();//Task
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();        
    }
}

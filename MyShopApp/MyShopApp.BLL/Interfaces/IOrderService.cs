using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task MakeOrdersAsync(OrderDTO orderDto, string id);
        Task MakeOrderAsync(OrderDTO orderDto);//Task
        IEnumerable<MotorcycleDTO> GetMotorcycles();//Task
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();        
        Task<MotorcycleDTO> GetMotorcycleAsync(int Id);//Task
    }
}

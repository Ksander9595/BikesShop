using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task MakeOrder(OrderDTO orderDto);//Task
        Task<MotorcycleDTO> GetMotorcycleAsync(int? id);//Task
        IEnumerable<MotorcycleDTO> GetMotorcycles();//Task
        IEnumerable<OrderDTO> GetOrders();
    }
}

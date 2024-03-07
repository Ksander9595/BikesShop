using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void MakeOrder(OrderDTO orderDto);
        MotorcycleDTO GetMotorcycle(int? id);
        IEnumerable<MotorcycleDTO> GetMotorcycles();
        
    }
}

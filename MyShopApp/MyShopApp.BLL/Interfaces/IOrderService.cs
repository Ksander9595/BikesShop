using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void MakeOrder(OrderDTO orderDto);
        MotorcycleDTO GetMotocycle(int? id);
        IEnumerable<MotorcycleDTO> GetMotocycles();
        
    }
}

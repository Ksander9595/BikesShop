using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void MakeOrder(OrderDTO orderDto);//Task
        MotorcycleDTO GetMotorcycle(int? id);//Task
        IEnumerable<MotorcycleDTO> GetMotorcycles();//Task
        
    }
}

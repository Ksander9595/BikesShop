using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        MotocycleDTO GetMotocycle(int? id);
        IEnumerable<MotocycleDTO> GetMotocycles();
        void Dispose();

    }
}

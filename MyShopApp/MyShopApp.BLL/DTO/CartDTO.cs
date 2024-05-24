

namespace MyShopApp.BLL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }
        public int MotorcycleId { get; set; }
        public List<CartLineDTO>? CartsLine { get; set; }
        public DateTime Date { get; set; }
    }
}

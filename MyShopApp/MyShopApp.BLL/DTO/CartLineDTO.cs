

namespace MyShopApp.BLL.DTO
{
    public class CartLineDTO
    {
        public int Id { get; set; }
        public string? MotorcycleName { get; set; }
        public string? MotorcycleModel { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

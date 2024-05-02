

namespace MyShopApp.BLL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MotorcycleId { get; set; }
        public string? MotorcycleName { get; set; }
        public string? MotorcycleModel { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}

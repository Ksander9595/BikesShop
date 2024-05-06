

namespace MyShopApp.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public int CartId { get; set; }
        public List<CartLineDTO> CartsLineDTO { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public int NumberCart { get; set; }
        public string? Validity { get; set; }
        public int CVV { get; set; }
    }
}

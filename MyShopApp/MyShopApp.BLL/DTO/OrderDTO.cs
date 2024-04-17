

using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? MotorcycleName { get; set; }
        public string? MotorcycleModel { get; set; }
        public int Quantity { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public List<CartLineDTO>? cartLineDTO { get; set; }
                 
       
        
    }
}

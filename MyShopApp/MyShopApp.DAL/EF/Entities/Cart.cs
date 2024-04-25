

namespace MyShopApp.DAL.EF.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public Motorcycle? motorcycle { get; set; } 
        public int OrderId { get; set; }
        public Order? order { get; set; }
        public int Quantity { get; set; }
    }
}

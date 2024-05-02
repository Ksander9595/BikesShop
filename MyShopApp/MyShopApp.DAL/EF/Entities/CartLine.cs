

namespace MyShopApp.DAL.EF.Entities
{
    public class CartLine
    {
        public int Id { get; set; }      
        public int MotorcycleId { get; set; }
        public Motorcycle? motorcycle { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        public int Quantity { get; set; }
    }
}

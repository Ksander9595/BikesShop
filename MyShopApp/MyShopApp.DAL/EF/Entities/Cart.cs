

namespace MyShopApp.DAL.EF.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }      
        public User? user { get; set; }
        public Order? order { get; set; }
        public List<CartLine>? CartLine { get; set; }
        public DateTime Date { get; set; }

    }
}



namespace MyShopApp.DAL.EF.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }        
        public int CartId { get; set; }
        public Cart? cart { get; set; }
        public DateTime Date { get; set; }
        public int NumberCart { get; set; }
        public string? Validity { get; set; }
        public int CVV { get; set; }
    }
}

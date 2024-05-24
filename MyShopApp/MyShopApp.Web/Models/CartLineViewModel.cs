namespace MyShopApp.Web.Models
{
    public class CartLineViewModel
    {
        public string? MotorcycleName { get; set; }
        public string? MotorcycleModel { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

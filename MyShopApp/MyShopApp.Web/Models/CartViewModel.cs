namespace MyShopApp.Web.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public List<CartLineViewModel>? cartLineViewModels { get; set; }
    }
}

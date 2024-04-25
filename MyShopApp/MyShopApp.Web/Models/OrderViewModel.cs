namespace MyShopApp.Web.Models
{
    public class OrderViewModel
    {
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }       
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public List<CartViewModel>? cartViewModels { get; set; }
    }
}

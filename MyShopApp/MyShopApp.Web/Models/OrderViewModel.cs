namespace MyShopApp.Web.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int MotocycleID { get; set; }
        public DateTime Date { get; set; }
    }
}

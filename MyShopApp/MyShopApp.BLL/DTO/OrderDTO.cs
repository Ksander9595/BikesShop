

using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }      
        //public string? FirstName { get; set; }
        //public string? SecondName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int MotorcycleID { get; set; }
        public DateTime Date { get; set; }
        //public List<Motorcycle>? Motorcycles { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set;}
        //public User? User { get; set; }
    }
}

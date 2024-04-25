using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopApp.DAL.EF.Entities
{
    public class ClientProfile
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}

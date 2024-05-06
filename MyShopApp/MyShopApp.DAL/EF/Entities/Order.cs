

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopApp.DAL.EF.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }
        public User? user { get; set; }
        public int CartId { get; set; }
        public Cart? cart { get; set; }
        [NotMapped]
        public Card? card { get; set; }
        public DateTime Date { get; set; }
       
    }
    [Keyless]
    public class Card
    {
        public int NumberCart { get; set; }
        public string? Validity { get; set; }
        public int CVV { get; set; }
    }
}

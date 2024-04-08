using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.EF.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public int UserId { get; set; }               
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public Motorcycle? Motorcycle { get; set; }             
        public User? User { get; set; }
        
    }
}

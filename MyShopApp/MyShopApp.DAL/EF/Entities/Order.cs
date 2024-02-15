using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.EF.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal Sum { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Motocycle motocycle { get; set; }
        public int MotocycleId { get; set; }

        public DateTime Date { get; set; }

        //public string UserId { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.EF.Entities
{
    public class CartLine
    {
        public int CardId { get; set; }
        public int MotorcycleId { get; set; }
        public Motorcycle? motorcycle { get; set; } 
        public int Quantity { get; set; }
    }
}

using MyShopApp.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.BLL.DTO
{
    public class CartLineDTO
    {
        public int CardId { get; set; }
        public int MotorcycleId { get; set; }
        public MotorcycleDTO? motorcycleDTO { get; set; }
        public int Quantity { get; set; }
    }
}

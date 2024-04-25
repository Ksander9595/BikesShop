using MyShopApp.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.BLL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }          
        public string? MotorcycleName { get; set; }
        public string? MotorcycleModel { get; set; }
        public int Quantity { get; set; }
    }
}

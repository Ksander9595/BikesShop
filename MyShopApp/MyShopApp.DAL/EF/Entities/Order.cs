using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.EF.Entities
{
    public class Order
    {
        public int Id { get; set; }       
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
       
    }
}

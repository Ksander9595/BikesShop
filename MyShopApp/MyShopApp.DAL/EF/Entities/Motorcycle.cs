using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.EF.Entities
{
    public class Motorcycle
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public MotorcycleClass motoClass { get; set; }
        public int Year { get; set; }
        public int Hp { get; set; }
        public int Capacity { get; set; }
        public bool Document { get; set; }
        public int Mileage { get; set; }
        public string? Color { get; set; }
        public bool Condition { get; set; }
        public bool Availability { get; set; }
        public List<Order>? Orders { get; set; }

    }
}

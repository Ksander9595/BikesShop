using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private ApplicationDbContext db;
        public OrderRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Order> GetAll()
        {
            return db.Orders.Include(o=>o.motocycle);
        }
        public Order Get(int id)
        {
            return db.Orders.FirstOrDefault(o => o.OrderId == id);
        }
        public void Create(Order order)
        {
            db.Orders.Add(order);
        }
        public void Update(Order order)
        {
            db.Orders.Update(order);
        }
        public void Delete(int id)
        {
            if (id != null)
            {
                Order? order = db.Orders.FirstOrDefault(o => o.OrderId == id);
                db.Orders.Remove(order);
            }
        }
        public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
        {
            return db.Orders
                .Include(o=>o.motocycle)
                .Where(predicate)
                .ToList();
        }
    }
}

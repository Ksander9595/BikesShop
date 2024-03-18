using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;

namespace MyShopApp.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>//GetAll, Find, Update async?
    {
        private ApplicationDbContext db;
        public OrderRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Order> GetAll()
        {            
            return db.Orders.Include(o=>o.motorcycle);            
        }
        public async Task<Order> GetAsync(int id)
        {
            return await db.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            
        }
        public async Task CreateAsync(Order order)
        {
            await db.Orders.AddAsync(order);           
        }
        public void Update(Order order)
        {
            db.Orders.Update(order);           
        }
        public async Task DeleteAsync(int id)
        {
            if (id != null)
            {
                Order? order = await db.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
                db.Orders.Remove(order);
            }           
        }
        public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
        {
            return db.Orders
                .Include(o=>o.motorcycle)
                .Where(predicate)
                .ToList();
            
        }
    }
}

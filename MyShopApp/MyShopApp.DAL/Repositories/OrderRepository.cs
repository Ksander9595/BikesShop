using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;

namespace MyShopApp.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>//??
    {
        private ApplicationDbContext db;
        public OrderRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {            
            var result = db.Orders.Include(o=>o.motorcycle);
            await db.SaveChangesAsync();
            return result;
        }
        public async Task<Order> GetAsync(int id)
        {
            var result = await db.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            await db.SaveChangesAsync();
            return result;
        }
        public async Task CreateAsync(Order order)
        {
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            db.Orders.Update(order);
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if (id != null)
            {
                Order? order = await db.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
                db.Orders.Remove(order);
            }
            await db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> FindAsync(Func<Order, Boolean> predicate)
        {
            var result = db.Orders
                .Include(o=>o.motorcycle)
                .Where(predicate)
                .ToList();
            await db.SaveChangesAsync();
            return result;
        }
    }
}

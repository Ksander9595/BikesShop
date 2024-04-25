using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MyShopApp.DAL.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private ApplicationDbContext db;
        public CartRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await db.Carts.ToListAsync();
        }
        public async Task<Cart> GetAsync(int id)
        {
            return await db.Carts.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateAsync(Cart cart)
        {
            await db.Carts.AddAsync(cart);
        }
        public void Update(Cart cart)
        {
            db.Carts.Update(cart);
        }
        public async Task DeleteAsync(int id)
        {
            if (id != null)
            {
                Cart? cart = await db.Carts.FirstOrDefaultAsync(m => m.Id == id);
                db.Carts.Remove(cart);
            }
        }
        public IEnumerable<Cart> Find(Func<Cart, Boolean> predicate)
        {
            return db.Carts.Where(predicate).ToList();
        }

    }
}

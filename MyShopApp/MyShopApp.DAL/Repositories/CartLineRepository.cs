using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MyShopApp.DAL.Repositories
{
        public class CartLineRepository : IRepository<CartLine>
        {
            private ApplicationDbContext db;
            public CartLineRepository(ApplicationDbContext context)
            {
                db = context;
            }
            public async Task<IEnumerable<CartLine>> GetAll()
            {
                return await db.CartsLine.Include(c=>c.motorcycle).ToListAsync();
            }
            public async Task<CartLine> GetAsync(int id)
            {
                return await db.CartsLine.FirstOrDefaultAsync(m => m.Id == id);
            }
            public async Task CreateAsync(CartLine cartLine)
            {
                await db.CartsLine.AddAsync(cartLine);
            }
            public void Update(CartLine cartLine)
            {
                db.CartsLine.Update(cartLine);
            }
            public async Task DeleteAsync(int id)
            {
                if (id != null)
                {
                    CartLine? cartLine = await db.CartsLine.FirstOrDefaultAsync(m => m.Id == id);
                    db.CartsLine.Remove(cartLine);
                }
            }
            public IEnumerable<CartLine> Find(Func<CartLine, Boolean> predicate)
            {
                return db.CartsLine.Where(predicate).ToList();
            }

        }
}

using Microsoft.EntityFrameworkCore;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class MotorcycleRepository : IRepository<Motorcycle>
    {
        private ApplicationDbContext db;
        public MotorcycleRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Motorcycle>> GetAll()
        {
            return await db.Motorcycles.ToListAsync();
        }
        public async Task<Motorcycle> GetAsync(int id)
        {
            return await db.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateAsync(Motorcycle motorcycle)
        {
            await db.Motorcycles.AddAsync(motorcycle);
        }
        public void Update(Motorcycle motorcycle)
        {
            db.Motorcycles.Update(motorcycle);           
        }
        public async Task DeleteAsync(int id)
        {
            if (id != null)
            {
                Motorcycle? motorcycle = await db.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);
                db.Motorcycles.Remove(motorcycle);
            }
        }
        public IEnumerable<Motorcycle> Find(Func<Motorcycle, Boolean> predicate)
        {            
            return db.Motorcycles.Where(predicate).ToList();
        }

    }
    
}

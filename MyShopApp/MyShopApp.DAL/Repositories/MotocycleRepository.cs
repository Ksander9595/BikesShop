using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class MotocycleRepository : IRepository<Motocycle>
    {
        private ApplicationDbContext db;
        public MotocycleRepository(ApplicationDbContext context) 
        {
            db = context;
        }
        public IEnumerable<Motocycle> GetAll()
        {
            return db.Motocycles;
        }
        public Motocycle Get(int id)
        {            
            return db.Motocycles.FirstOrDefault(m => m.Id == id);
        }
        public void Create(Motocycle motocycle)
        {
            db.Motocycles.Add(motocycle);
        }
        public void Update(Motocycle motocycle)
        {
            db.Motocycles.Update(motocycle);
        }
        public void Delete(int id)
        {
            if(id != null)
            {
                Motocycle? motocycle = db.Motocycles.FirstOrDefault(m => m.Id == id);
                db.Motocycles.Remove(motocycle);
            }           
        }
        public IEnumerable<Motocycle> Find(Func<Motocycle, Boolean> predicate)
        {
            return db.Motocycles.Where(predicate).ToList();
        }


    }
}

using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class EFUnitOfWorks : IUnitOfWork
    {
        private ApplicationDbContext db;

        private MotorcycleRepository motorcycleRepository;
        private OrderRepository orderRepository;

        public EFUnitOfWorks(ApplicationDbContext context)
        {
            db = context;
        }
        public IRepository<Motorcycle> Motorcycles
        {
            get 
            { 
                if(motorcycleRepository == null)
                    motorcycleRepository = new MotorcycleRepository(db);
                return  motorcycleRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }    
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);    
        }
    }
}

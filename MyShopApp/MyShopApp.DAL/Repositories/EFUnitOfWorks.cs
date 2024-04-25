using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class EFUnitOfWorks : IUnitOfWork
    {
        private ApplicationDbContext db;

        private CartRepository cartRepository;
        private MotorcycleRepository motorcycleRepository;
        private OrderRepository orderRepository;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        private SignInManager<User> signInManager;
        private IClientManager clientManager;

        public EFUnitOfWorks(
            ApplicationDbContext context, UserManager<User> UserManager, RoleManager<Role> RoleManager, SignInManager<User> SignInManager,
            IClientManager ClientManager)
        {
            db = context;
            userManager = UserManager;
            roleManager = RoleManager;
            signInManager = SignInManager;
            clientManager = ClientManager;
        }
        public IRepository<Cart> Carts
        {
            get
            {
                if (cartRepository == null)
                    cartRepository = new CartRepository(db);
                return cartRepository;
            }
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
        public UserManager<User> UserManager { get { return userManager; } }
        public RoleManager<Role> RoleManager { get { return roleManager; } }
        public SignInManager<User> SignInManager { get { return signInManager; } }
        public IClientManager ClientManager { get { return clientManager; } }
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
                    userManager.Dispose();
                    roleManager.Dispose();                   
                    clientManager.Dispose();
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

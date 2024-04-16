using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class IdentityUnitOfWork : IidentityUnitOfWork
    {
        private ApplicationDbContext db;
       
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        private SignInManager<User> signInManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(
            ApplicationDbContext Database,            
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            SignInManager<User> SignInManager,
            IClientManager ClientManager
            )
        {
            db = Database;
            userManager = UserManager;
            roleManager = RoleManager;
            signInManager = SignInManager;
            clientManager = ClientManager;
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
            if (!this.disposed)
            {               
                 if (disposing)
                 {
                    userManager.Dispose();
                    roleManager.Dispose();
                    //signInManager.Dispose();
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

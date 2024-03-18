using Microsoft.AspNetCore.Identity;
using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Identity;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class IdentityUnitOfWork : IidentityUnitOfWork
    {
        private ApplicationDbContext db;

        //private ApplicationUserManager userManager;
        //private ApplicationRoleManager roleManager;
        //private ApplicationSignInManager signInManager;
        private UserManager<User> userManager;
        private RoleManager<Role> roleManager;
        private SignInManager<User> signInManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(ApplicationDbContext Database,
            //ApplicationUserManager UserManager, 
            //ApplicationRoleManager RoleManager, 
            //ApplicationSignInManager SignInManager,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            SignInManager<User> SignInManager,

        IClientManager ClientManager)
        {
            db = Database;
            userManager = UserManager;
            roleManager = RoleManager;
            signInManager = SignInManager;
            clientManager = ClientManager;
        }

        //public ApplicationUserManager UserManager { get { return userManager; } }
        //public ApplicationRoleManager RoleManager { get { return roleManager; } }
        //public ApplicationSignInManager SignInManager { get { return signInManager; } }

        public UserManager<User> UserManager { get { return userManager; } }
        public RoleManager<Role> RoleManager { get { return roleManager; } }
        public SignInManager<User> SignInManager { get { return signInManager; } }
        public IClientManager ClientManager { get { return clientManager; } }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
    }
}

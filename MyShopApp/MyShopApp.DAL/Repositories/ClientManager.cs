using MyShopApp.DAL.EF;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationDbContext Database { get; set; }
        public ClientManager(ApplicationDbContext db) {  Database = db; }

        public async Task CreateAsync(ClientProfile item)
        {
            await Database.ClientProfiles.AddAsync(item);
            await Database.SaveChangesAsync();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

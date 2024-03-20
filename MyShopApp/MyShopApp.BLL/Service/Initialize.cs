using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;


namespace MyShopApp.BLL.Service
{
    public class Initialize
    {
        IidentityUnitOfWork Database;

        public Initialize(IidentityUnitOfWork db)
        {
            Database = db;
        }

        public async Task UserInitialize()
        {
            string password = "Qwerty123!";
            var admin = new User
            {
                UserName = "admin@mail.ru",
                Email = "admin@mail.ru",
            };
            if (await Database.UserManager.FindByEmailAsync(admin.Email) == null)
            {
                await Database.UserManager.CreateAsync(admin, password);
                await Database.UserManager.AddToRoleAsync(admin, "admin");
            }
            else
                await Database.UserManager.AddToRoleAsync(admin, "admin");

            ClientProfile clientProfile = new ClientProfile { Id = admin.Id, Name = admin.UserName };
            await Database.ClientManager.CreateAsync(clientProfile);
            await Database.SaveAsync();           
        }
    }
}

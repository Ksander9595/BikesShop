using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;



namespace MyShopApp.BLL.Service
{
    public class UserService : IUserService
    {
        IidentityUnitOfWork Database { get; set; }
        
        public UserService(IidentityUnitOfWork db) 
        {          
            Database = db;
        }
      
        public async Task<UserDTO> GetUserAsync(string Id)
        {
            User? user = await Database.UserManager.FindByIdAsync(Id);

            return new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Name = user.Email,
                Year = user.Year               
            };                       
        }      
        public IEnumerable<UserDTO> GetUsers()
        {            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.UserManager.Users);

        }
        public async Task<OperationDetails> CreateAsync(UserDTO userDTO, string password)
        {
            User? user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new User { Email = userDTO.Email, UserName = userDTO.Email, Year = userDTO.Year };
                var result = await Database.UserManager.CreateAsync(user, password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDTO.Address, Name = userDTO.Name };
                await Database.ClientManager.CreateAsync(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successful", "");
            }
            else
            {
                return new OperationDetails(false, "A user with this login already exists", "Email");
            }  
        }      
        public async Task<IdentityResult> UpdateAsync(UserDTO userDTO)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            if(user!=null)
            {
                user.Email = userDTO.Email;
                user.UserName = userDTO.Email;
                user.Year = userDTO.Year;
            }
            return await Database.UserManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> DeleteAsync(string Id)
        {
            User? user = await Database.UserManager.FindByIdAsync(Id);          
            return await Database.UserManager.DeleteAsync(user);                             
        }
        public async Task<IdentityResult> ChangePasswordAsync(UserDTO userDTO, string oldPass, string newPass)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            return await Database.UserManager.ChangePasswordAsync(user, oldPass, newPass);

        }
        public async Task<IList<string>> GetUserRolesAsync(UserDTO userDTO)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            return await Database.UserManager.GetRolesAsync(user);
        }
        public async Task<IdentityResult> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            return await Database.UserManager.AddToRolesAsync(user, addedRoles);
        }
        public async Task<IdentityResult> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            return await Database.UserManager.RemoveFromRolesAsync(user, removeRoles);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

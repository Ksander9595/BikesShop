using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;



namespace MyShopApp.BLL.Service
{
    public class UserService : IUserService
    {
        UserManager<User> userManager;               
        
        public UserService(UserManager<User> _userManager) 
        {
            userManager = _userManager;            
        }
      
        public async Task<UserDTO> GetUserAsync(string Id)
        {
            User? user = await userManager.FindByIdAsync(Id);

            return new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Name = user.Email,
                Year = user.Year               
            };                       
        }      
        public IEnumerable<UserDTO> GetUsersAsync()
        {            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(userManager.Users);

        }
        public async Task<IdentityResult> CreateAsync(UserDTO userDTO, string password)
        {
            User user = new User { Email = userDTO.Email, UserName = userDTO.Email, Year = userDTO.Year };
            return await userManager.CreateAsync(user, password);
        }      
        public async Task<IdentityResult> UpdateAsync(UserDTO userDTO)
        {
            User? user = await userManager.FindByIdAsync(userDTO.Id);
            if(user!=null)
            {
                user.Email = userDTO.Email;
                user.UserName = userDTO.Email;
                user.Year = userDTO.Year;
            }
            return await userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> DeleteAsync(string Id)
        {
            User? user = await userManager.FindByIdAsync(Id);          
            return await userManager.DeleteAsync(user);                             
        }
        public async Task<IdentityResult> ChangePasswordAsync(UserDTO userDTO, string oldPass, string newPass)
        {
            User? user = await userManager.FindByIdAsync(userDTO.Id);
            return await userManager.ChangePasswordAsync(user, oldPass, newPass);

        }
        public async Task<IList<string>> GetUserRolesAsync(UserDTO userDTO)
        {
            User? user = await userManager.FindByIdAsync(userDTO.Id);
            return await userManager.GetRolesAsync(user);
        }
        public async Task<IdentityResult> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles)
        {
            User? user = await userManager.FindByIdAsync(userDTO.Id);
            return await userManager.AddToRolesAsync(user, addedRoles);
        }
        public async Task<IdentityResult> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles)
        {
            User? user = await userManager.FindByIdAsync(userDTO.Id);
            return await userManager.RemoveFromRolesAsync(user, removeRoles);
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
                }
                this.disposed = true;
            }
        }
    }
}

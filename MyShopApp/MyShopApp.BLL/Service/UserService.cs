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
      
        public async Task<UserDTO> GetUser(string Id)
        {
            User? user = await userManager.FindByIdAsync(Id);

            return new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Name = user.Email,
                Year = user.Year               
            };                       
        }      
        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg=>cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(userManager.Users);
        }
        public async Task<IdentityResult> Create(UserDTO userDTO)
        {
            User user = new User { Email = userDTO.Email, UserName = userDTO.Email, Year = userDTO.Year };
            return await userManager.CreateAsync(user, userDTO.Password);
        }      
        public async Task<IdentityResult> Update(UserDTO userDTO)
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
        public async Task<IdentityResult> Delete(string Id)
        {
            User? user = await userManager.FindByIdAsync(Id);                       
            return await userManager.DeleteAsync(user);
            
        }
    }
}

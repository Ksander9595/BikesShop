using AutoMapper;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using Microsoft.AspNetCore.Identity;



namespace MyShopApp.BLL.Service
{
    public class UserService : IUserService //GetUsers and GetUsersRoleAsync
    {
        IidentityUnitOfWork Database;
        
        public UserService(IidentityUnitOfWork db) 
        {          
            Database = db;
        }
      
        public async Task<UserDTO> GetUserIdAsync(string Id)
        {
            User? user = await Database.UserManager.FindByIdAsync(Id);

            return new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Name = user.Email,
                DateOfBirth = user.DateOfBirth               
            };                       
        }  

        public async Task<UserDTO> GetUserNameAsync(string Name)
        {
            User? user = await Database.UserManager.FindByEmailAsync(Name);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Email,
                DateOfBirth = user.DateOfBirth,
                Zip = user.Zip,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };
        }
        
        public IEnumerable<UserDTO> GetUsers()
        {            
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.UserManager.Users);
        }     

        public async Task<OperationDetails> CreateUserAsync(UserDTO userDTO)
        {
            User? user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {                
                user = new User 
                { 
                    Email = userDTO.Email, 
                    UserName = userDTO.Email,
                    DateOfBirth = userDTO.DateOfBirth, 
                    Address = userDTO.Address, 
                    PhoneNumber = userDTO.PhoneNumber, 
                    Zip = userDTO.Zip 
                };
                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");

                    await Database.UserManager.AddToRoleAsync(user, "user");
                
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDTO.Address, Name = userDTO.Name };
                await Database.ClientManager.CreateAsync(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration user successful", "");
            }
            else
            {
                return new OperationDetails(false, "A user with this login already exists", "Email");
            }  
        } 
        
        public async Task<OperationDetails> UpdateUserAsync(UserDTO userDTO)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            if(user != null)
            {
                user.Email = userDTO.Email;
                user.UserName = userDTO.Email;
                user.DateOfBirth = userDTO.DateOfBirth;
                user.Zip = userDTO.Zip;
                user.Address = userDTO.Address;
                user.DateOfBirth = userDTO.DateOfBirth;
                var result = await Database.UserManager.UpdateAsync(user);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Updated successful", "");
            }
            else            
                return new OperationDetails(false, "Updated error", "Email");            
        }

        public async Task<OperationDetails> DeleteUserAsync(string Id)
        {
            User? user = await Database.UserManager.FindByIdAsync(Id); 
            if( user != null )
            {
                var result = await Database.UserManager.DeleteAsync(user);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Deleted user successful", "");
            }
            else
            {
                return new OperationDetails(false, "Deleted user error", "Email");
            }                                        
        }

        public async Task<OperationDetails> ChangePasswordAsync(UserDTO userDTO, string oldPass, string newPass)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            if(user != null )
            {
                var result = await Database.UserManager.ChangePasswordAsync(user, oldPass, newPass);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Password changed successful", "");
            }
            else
            {
                return new OperationDetails(false, "Password change error", "Email");
            }
        }        

        public async Task SignIn(UserDTO userDTO, bool value)
        {
            User? user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            await Database.SignInManager.SignInAsync(user, value);
        }

        public async Task<OperationDetails> PasswordSignIn(UserDTO userDTO, bool RememberMe, bool value)
        {
            var result = await Database.SignInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, RememberMe, value);
            if (result.Succeeded)
                return new OperationDetails(true, "User sign in successful", "");
            else
                return new OperationDetails(false, "User sign in error", "Email");
        }

        public async Task SignOutAsync()
        {
            await Database.SignInManager.SignOutAsync();
        }

        public async Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles)
        {
            foreach(string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    role = new Role { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await CreateUserAsync(adminDto);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

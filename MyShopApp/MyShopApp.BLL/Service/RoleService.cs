using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.BLL.DTO;
using AutoMapper;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Service
{
    public class RoleService : IRoleService
    {

        IidentityUnitOfWork Database;

        public RoleService(IidentityUnitOfWork db)
        {
            Database = db;
        }
        public async Task RoleInitialization()
        {
            string adminEmail = "Admin@mail.ru";
            string password = "Qwerty123!";
            if (await Database.RoleManager.FindByNameAsync("admin") == null)
                await Database.RoleManager.CreateAsync(new Role { Name = "admin" });
            if (await Database.RoleManager.FindByNameAsync("user") == null)
                await Database.RoleManager.CreateAsync(new Role { Name = "user" });
            if (await Database.UserManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = await Database.UserManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await Database.UserManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
        public async Task<RoleDTO> GetRoleAsync(int Id)
        {
            Role? role = await Database.RoleManager.FindByIdAsync(Id.ToString());
            return new RoleDTO
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IdentityRole, RoleDTO>()).CreateMapper();
            //return mapper.Map<IEnumerable<IdentityRole>, List<RoleDTO>>(Database.RoleManager.Roles);
            var roles = Database.RoleManager.Roles.ToList();
            var rolesDTO = new List<RoleDTO>();
            foreach (var role in roles)
            {
                RoleDTO roleDTO = new RoleDTO();
                roleDTO.RoleId = role.Id;
                roleDTO.RoleName = role.Name;
                rolesDTO.Add(roleDTO);
            }
            return rolesDTO;
        }

        public async Task<OperationDetails> CreateRoleAsync(RoleDTO roleDTO)
        {
            Role? role = await Database.RoleManager.FindByNameAsync(roleDTO.RoleName);
            if (role == null)
            {
                role = new Role { Name = roleDTO.RoleName };
                var result = await Database.RoleManager.CreateAsync(role);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration role successful", "");
            }
            else
            {
                return new OperationDetails(false, "This role already exist", "Role");
            }
        }
        public async Task<OperationDetails> DeleteRoleAsync(RoleDTO roleDTO)
        {
            Role? role = await Database.RoleManager.FindByNameAsync(roleDTO.RoleName);
            if (role == null)
            {
                var result = await Database.RoleManager.DeleteAsync(role);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Deleted role successful", "");
            }
            else
            {
                return new OperationDetails(false, "Deleted role error", "Role");
            }
        }
        public async Task<IList<string>> GetUserRolesAsync(UserDTO userDTO)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id.ToString());
            return await Database.UserManager.GetRolesAsync(user);
        }
        public async Task<RoleDTO> FindByNameAsync(string name)
        {
            var result = await Database.RoleManager.FindByNameAsync(name);
            if (result != null)
                return new RoleDTO { RoleName = name };
            else
                return new RoleDTO { RoleName = null };
        }
        public async Task<OperationDetails> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id.ToString());
            if (user != null)
            {
                var result = await Database.UserManager.AddToRolesAsync(user, addedRoles);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Roles added successful", "");
            }
            else
            {
                return new OperationDetails(false, "Roles added error", "Role");
            }
        }
        public async Task<OperationDetails> AddToRoleAsync(UserDTO userDTO, string role)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id.ToString());
            if (user != null)
            {
                var result = await Database.UserManager.AddToRoleAsync(user, role);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Role added successful", "");
            }
            else
            {
                return new OperationDetails(false, "Role added error", "Role");
            }
        }

        public async Task<OperationDetails> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles)
        {
            User? user = await Database.UserManager.FindByIdAsync(userDTO.Id.ToString());
            if (user != null)
            {
                var result = await Database.UserManager.RemoveFromRolesAsync(user, removeRoles);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");
                await Database.SaveAsync();
                return new OperationDetails(true, "Roles remove successful", "");
            }
            else
            {
                return new OperationDetails(false, "Roles remove error", "Role");
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }


}

using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.BLL.DTO;
using AutoMapper;

namespace MyShopApp.BLL.Service
{
    public class RoleService : IRoleService
    {
        
        RoleManager<IdentityRole> roleManager;
        
        public RoleService(RoleManager<IdentityRole> _roleManager)
        {            
            roleManager = _roleManager;
        }
        public async Task<RoleDTO> GetRole(string Id)
        {
            IdentityRole? role = await roleManager.FindByIdAsync(Id);
            return new RoleDTO
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
        public IEnumerable<RoleDTO> GetRoles()
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IdentityRole, RoleDTO>()).CreateMapper();
            //return mapper.Map<IEnumerable<IdentityRole>, List<RoleDTO>>(roleManager.Roles);
            var roles = roleManager.Roles.ToList();
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
        public async Task<IdentityResult> Create(RoleDTO roleDTO)
        {
            IdentityRole role = new IdentityRole { Name = roleDTO.RoleName };
            return await roleManager.CreateAsync(role);           
        }
        public async Task<IdentityResult> Delete(RoleDTO roleDTO)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleDTO.RoleId);
            return await roleManager.DeleteAsync(role);
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
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
    
    
}

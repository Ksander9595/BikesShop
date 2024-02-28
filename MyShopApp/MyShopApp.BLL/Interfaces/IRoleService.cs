using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<RoleDTO> GetRole(string Id);
        IEnumerable<RoleDTO> GetRoles();
        Task<IdentityResult> Create(RoleDTO roleDTO);
        Task<IdentityResult> Delete(RoleDTO roleDTO);
        
    }
}

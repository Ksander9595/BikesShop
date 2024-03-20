using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<RoleDTO> GetRoleAsync(string Id);
        IEnumerable<RoleDTO> GetRoles();
        Task<OperationDetails> CreateRoleAsync(RoleDTO roleDTO);
        Task<OperationDetails> DeleteRoleAsync(RoleDTO roleDTO);
        Task<IList<string>> GetUserRolesAsync(UserDTO userDTO);
        Task<RoleDTO> FindByNameAsync(string name);
        Task<OperationDetails> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles);
        Task<OperationDetails> AddToRoleAsync(UserDTO userDTO, string role);
        Task<OperationDetails> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles);
    }
}

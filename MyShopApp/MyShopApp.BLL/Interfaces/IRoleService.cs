using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<RoleDTO> GetRole(string Id);
        IEnumerable<RoleDTO> GetRoles();//Task
        Task<OperationDetails> Create(RoleDTO roleDTO);
        Task<OperationDetails> Delete(RoleDTO roleDTO);
        
    }
}

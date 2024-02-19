

using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Delete(string Id);
        Task<IdentityResult> Create(UserDTO userDTO);
        Task<IdentityResult> Update(UserDTO userDTO);
        IEnumerable<UserDTO> GetUsers();
        Task<UserDTO> GetUser(string Id);
    }
}



using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Delete(string Id);
        Task<IdentityResult> Create(UserDTO userDTO, string password);
        Task<IdentityResult> Update(UserDTO userDTO);
        IEnumerable<UserDTO> GetUsers();
        Task<UserDTO> GetUser(string Id);
        Task<IdentityResult> ChangePassword(UserDTO userDTO, string oldPassword, string newPassword);
        void Dispose();
    }
}

using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> DeleteAsync(string Id);
        Task<IdentityResult> CreateAsync(UserDTO userDTO, string password);
        Task<IdentityResult> UpdateAsync(UserDTO userDTO);
        IEnumerable<UserDTO> GetUsersAsync();
        Task<UserDTO> GetUserAsync(string Id);
        Task<IList<string>> GetUserRolesAsync(UserDTO userDTo);
        Task<IdentityResult> ChangePasswordAsync(UserDTO userDTO, string oldPassword, string newPassword);
        Task<IdentityResult> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles);
        Task<IdentityResult> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles);
    }
}

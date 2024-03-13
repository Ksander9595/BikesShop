using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<UserDTO> GetUserAsync(string Id);
        IEnumerable<UserDTO> GetUsers();
        Task<RoleDTO> GetRoleAsync(string Id);
        IEnumerable<RoleDTO> GetRoles();
        Task<OperationDetails> CreateRoleAsync(RoleDTO roleDTO);
        Task<OperationDetails> DeleteRoleAsync(RoleDTO roleDTO);
        Task<OperationDetails> CreateUserAsync(UserDTO userDTO);
        Task<OperationDetails> UpdateUserAsync(UserDTO userDTO);
        Task<OperationDetails> DeleteUserAsync(string Id);
        Task<OperationDetails> ChangePasswordAsync(UserDTO userDTO, string oldPass, string newPass);
        Task<IList<string>> GetUserRolesAsync(UserDTO userDTO);
        Task<OperationDetails> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles);
        Task<OperationDetails> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles);
        Task SignIn(UserDTO userDTO, bool value);
        Task<OperationDetails> PasswordSignIn(UserDTO userDTO, bool RememberMe, bool value);
        Task SignOutAsync();
        Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles);
    }
}

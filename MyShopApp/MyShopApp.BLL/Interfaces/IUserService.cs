using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task SetInititalData(UserDTO adminDto, List<string> roles);
        Task<OperationDetails> DeleteAsync(string Id);
        Task<OperationDetails> CreateAsync(UserDTO userDTO, string password);
        Task<OperationDetails> UpdateAsync(UserDTO userDTO);
        IEnumerable<UserDTO> GetUsers();//Task
        Task<UserDTO> GetUserAsync(string Id);
        Task<IList<string>> GetUserRolesAsync(UserDTO userDTo);
        Task<OperationDetails> ChangePasswordAsync(UserDTO userDTO, string oldPassword, string newPassword);
        Task<OperationDetails> AddToRolesAsync(UserDTO userDTO, IEnumerable<string> addedRoles);
        Task<OperationDetails> RemoveFromRolesAsync(UserDTO userDTO, IEnumerable<string> removeRoles);
    }
}

using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;

namespace MyShopApp.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<UserDTO> GetUserIdAsync(int Id);
        Task<UserDTO> GetUserNameAsync(string UserName);
        IEnumerable<UserDTO> GetUsers();              
        Task<OperationDetails> CreateUserAsync(UserDTO userDTO);
        Task<OperationDetails> UpdateUserAsync(UserDTO userDTO);
        Task<OperationDetails> DeleteUserAsync(int Id);
        Task<OperationDetails> ChangePasswordAsync(UserDTO userDTO, string oldPass, string newPass);       
        Task SignIn(UserDTO userDTO, bool value);
        Task<OperationDetails> PasswordSignIn(UserDTO userDTO, bool RememberMe, bool value);
        Task SignOutAsync();
        Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles);
    }
}

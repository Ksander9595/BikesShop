using Microsoft.AspNetCore.Identity;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.Service;

namespace MyShopApp.BLL.Interfaces
{
    public interface ISignInService : IDisposable
    {
        Task SignIn(UserDTO userDTO, bool value);
        Task<OperationDetails> PasswordSignIn(UserDTO userDTO, bool RememberMe, bool value);
        Task SignOutAsync();
    }
}

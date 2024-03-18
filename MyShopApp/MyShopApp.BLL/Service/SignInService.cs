//using Microsoft.AspNetCore.Identity;
//using MyShopApp.BLL.DTO;
//using MyShopApp.BLL.Interfaces;
//using MyShopApp.DAL.EF.Entities;

//namespace MyShopApp.BLL.Service
//{
//    public class SignInService : ISignInService
//    {
//        SignInManager<User> signInManager;
//        UserManager<User> userManager;

//        public SignInService(SignInManager<User> _signInManager, UserManager<User> _userManager)
//        {
//            signInManager = _signInManager;
//            userManager = _userManager;
//        }
//        public async Task SignIn(UserDTO userDTO, bool value)
//        {
//            User? user = await userManager.FindByIdAsync(userDTO.Id);
//            await signInManager.SignInAsync(user, value);
//        }
//        public async Task<SignInResult> PasswordSignIn(UserDTO userDTO, bool RememberMe, bool value)
//        {
//            return await signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, RememberMe, value);
//        }
//        public async Task SignOutAsync()
//        {
//            await signInManager.SignOutAsync();
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        private bool disposed = false;

//        public virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    userManager.Dispose();

//                }
//                this.disposed = true;
//            }
//        }
//    }
//}

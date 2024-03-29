using Microsoft.AspNetCore.Mvc;
using MyShopApp.Web.Models;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Service;

namespace MyShopApp.Web.Controllers
{
    public class AccountController : Controller
    {
        
        IUserService userService;      

        public AccountController(IUserService _userService)
        {
            userService = _userService;             
        }


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO? userDTO = new UserDTO 
                { 
                    Email = model.Email, 
                    Name = model.Email, 
                    Password = model.Password, 
                    Address = model.Address, 
                    Year = model.Year,
                    Zip = model.Zip
                };    
                
                var result = await userService.CreateUserAsync(userDTO);
                if(result.Succeeded)
                {                   
                    await userService.SignIn(userDTO, false);//устанавливаются аутентификационные куки
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ModelState.AddModelError(result.Property, result.Message);                                     
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserDTO? userDTO = new UserDTO { Email = model.Email, Password = model.Password };
                bool RememberMe = model.RememberMe;
                var result = await userService.PasswordSignIn(userDTO, RememberMe, false);//аутентификация пользователя

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))                    
                        return Redirect(model.ReturnUrl);                    
                    else                    
                        return RedirectToAction("HomePage", "Home");                                       
                }
                else
                {
                    ModelState.AddModelError(result.Property, result.Message);
                }                 
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await userService.SignOutAsync();//удаляет аутентификационные куки
            return RedirectToAction("HomePage", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}

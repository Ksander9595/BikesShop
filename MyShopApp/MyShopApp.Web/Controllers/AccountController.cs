using Microsoft.AspNetCore.Mvc;
using MyShopApp.Web.Models;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.DTO;

namespace MyShopApp.Web.Controllers
{
    public class AccountController : Controller
    {
        
        IUserService userService;       
        ISignInService signInService;

        public AccountController(IUserService _userService, ISignInService _signInService)
        {
            userService = _userService;
            signInService = _signInService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO? userDTO = new UserDTO { Email = model.Email, Name = model.Email, Year = model.Year };

                var result = await userService.Create(userDTO, model.Password);
                if(result.Succeeded)
                {
                    await signInService.SignIn(userDTO, false);//устанавливаются аутентификационные куки
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
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
                var result = await signInService.PasswordSignIn(userDTO, RememberMe, false);//аутентификация пользователя

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("HomePage", "Home");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and (or) password");
                }    
               
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInService.SignOutAsync();//удаляет аутентификационные куки
            return RedirectToAction("HomePage", "Home");
        }                    
    }
}

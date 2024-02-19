using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Service;
using MyShopApp.Web.Models;

namespace MyShopApp.Web.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        
        public UsersController(UserService _userService)
        {
            userService = _userService;
        }
        public IActionResult UserPage()
        {
            return View(User);
        }
        public IActionResult UserList() => View(userService.GetUsers());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO { Email = model.Email, Name = model.Email, Year = model.Year };               
                var result = await userService.Create(user);               
                if(result.Succeeded)
                {
                    return RedirectToAction("UserList");
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
        public async Task<IActionResult> Edit(string id)
        {
            UserDTO userDTO = await userService.GetUser(id);
            if(userDTO == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = userDTO.Id, Email = userDTO.Email, Year = userDTO.Year };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO? userDTO = await userService.GetUser(model.Id);
                if(userDTO!=null)
                {
                    userDTO.Email = model.Email;
                    userDTO.Name = model.Email;
                    userDTO.Year = model.Year;

                    var result = await userService.Update(userDTO);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await userService.Delete(id);     
            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> ChangePassword(string Id)
        {
            User? user = await _userManager.FindByIdAsync(Id);
            if(user==null )
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userManager.FindByIdAsync(model.Id); 
                if(user!=null) 
                {
                    var _passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult? result = await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if(result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not found");
                }               
            }
            return View(model);
        }
    }
}

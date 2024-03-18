using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.Web.Models;

namespace MyShopApp.Web.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        
        public UsersController(IUserService _userService)
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
                UserDTO userDTO = new UserDTO { Email = model.Email, Name = model.Email, Year = model.Year, Password = model.Password };               
                var result = await userService.CreateUserAsync(userDTO);               
                if(result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {                                        
                    ModelState.AddModelError(result.Property, result.Message);                    
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            UserDTO userDTO = await userService.GetUserAsync(id);
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
                UserDTO? userDTO = await userService.GetUserAsync(model.Id);
                if(userDTO!=null)
                {
                    userDTO.Email = model.Email;
                    userDTO.Name = model.Email;
                    userDTO.Year = model.Year;

                    var result = await userService.UpdateUserAsync(userDTO);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {                       
                        ModelState.AddModelError(result.Property, result.Message);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else
            {
               ModelState.AddModelError(result.Property, result.Message);
            }
            return View();//?
        }

        public async Task<IActionResult> ChangePassword(string Id)
        {
            UserDTO userDTO = await userService.GetUserAsync(Id);
            if(userDTO==null )
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = userDTO.Id, Email = userDTO.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO? userDTO = await userService.GetUserAsync(model.Id); 
                if(userDTO!=null)
                {
                    var result = await userService.ChangePasswordAsync(userDTO, model.OldPassword, model.NewPassword);
                    if(result.Succeeded)
                    {                        
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        ModelState.AddModelError(result.Property, result.Message);
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

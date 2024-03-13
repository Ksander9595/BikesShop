using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;

using MyShopApp.Web.Models;

namespace MyShopApp.Web.Controllers
{
    public class RolesController : Controller
    {
               
        IUserService userService;
        public RolesController(IUserService _userService)
        {            
            userService = _userService;
        }

        public IActionResult RolesList()
        {
            return View(userService.GetRoles());
        }
        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                RoleDTO roleDTO = new RoleDTO { RoleName = name };
                var result = await userService.CreateRoleAsync(roleDTO);               
                if (result.Succedeed)
                    return RedirectToAction("RolesList");
                else                  
                    ModelState.AddModelError(result.Property, result.Message);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoles(string id)
        {
            RoleDTO? roleDTO = await userService.GetRoleAsync(id);
            if(roleDTO != null) 
            {
               var result = await userService.DeleteRoleAsync(roleDTO);
                if (result.Succedeed)
                    return RedirectToAction("RolesList");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }
            return View();
        }

        public async Task<IActionResult> EditRoles(string userId)
        {
            UserDTO? userDTO = await userService.GetUserAsync(userId);           
            if(userDTO!= null)
            {
                var userRoles = await userService.GetUserRolesAsync(userDTO);
                var allRoles = userService.GetRoles();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = userDTO.Id,
                    UserEmail = userDTO.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditRoles(string userId, List<string> roles)
        {
            UserDTO? userDTO = await userService.GetUserAsync(userId);
            if(userDTO!= null)
            {
                var userRoles = await userService.GetUserRolesAsync(userDTO);
                var allRoles = userService.GetRoles();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await userService.AddToRolesAsync(userDTO, addedRoles);

                await userService.RemoveFromRolesAsync(userDTO, removedRoles);

                return RedirectToAction("UserList", "Users");
            }
            return NotFound();
        }
    }
}

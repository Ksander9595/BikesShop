using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;

using MyShopApp.Web.Models;

namespace MyShopApp.Web.Controllers
{
    public class RolesController : Controller
    {
        
        IRoleService roleService;
        IUserService userService;
        public RolesController(IRoleService _roleService, IUserService _userService)
        {
            roleService = _roleService;
            userService = _userService;
        }

        public IActionResult RolesList()
        {
            return View(roleService.GetRoles());
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                RoleDTO roleDTO = new RoleDTO { RoleName = name };
                var result = await roleService.Create(roleDTO);               
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            RoleDTO? roleDTO = await roleService.GetRole(id);
            if(roleDTO != null) 
            {
               var result = await roleService.Delete(roleDTO);
            }
            return RedirectToAction("RolesList");
        }

        public async Task<IActionResult> Edit(string userId)
        {
            UserDTO? userDTO = await userService.GetUserAsync(userId);           
            if(userDTO!= null)
            {
                var userRoles = await userService.GetUserRolesAsync(userDTO);
                var allRoles = roleService.GetRoles();
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
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            UserDTO? userDTO = await userService.GetUserAsync(userId);
            if(userDTO!= null)
            {
                var userRoles = await userService.GetUserRolesAsync(userDTO);
                var allRoles = roleService.GetRoles();
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

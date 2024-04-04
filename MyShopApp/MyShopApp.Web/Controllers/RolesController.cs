using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.Web.Models;

namespace MyShopApp.Web.Controllers
{
    public class RolesController : Controller
    {
        IUserService userService;       
        IRoleService roleService;
        public RolesController(IRoleService _roleService, IUserService _userService)
        {
            userService = _userService;
            roleService = _roleService;
        }

        public IActionResult RolesList()
        {
            return View(roleService.GetRoles());
        }
        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                RoleDTO roleDTO = new RoleDTO { RoleName = name };
                var result = await roleService.CreateRoleAsync(roleDTO);               
                if (result.Succeeded)
                    return RedirectToAction("RolesList");
                else                  
                    ModelState.AddModelError(result.Property, result.Message);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoles(string id)
        {
            RoleDTO? roleDTO = await roleService.GetRoleAsync(id);
            if(roleDTO != null) 
            {
               var result = await roleService.DeleteRoleAsync(roleDTO);
                if (result.Succeeded)
                    return RedirectToAction("RolesList");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }
            return View();
        }

        public async Task<IActionResult> EditRoles(string userId)
        {
            UserDTO? userDTO = await userService.GetUserIdAsync(userId);           
            if(userDTO!= null)
            {
                var userRoles = await roleService.GetUserRolesAsync(userDTO);
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
        public async Task<IActionResult> EditRoles(string userId, List<string> roles)
        {
            UserDTO? userDTO = await userService.GetUserIdAsync(userId);
            if(userDTO!= null)
            {
                var userRoles = await roleService.GetUserRolesAsync(userDTO);
                var allRoles = roleService.GetRoles();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await roleService.AddToRolesAsync(userDTO, addedRoles);

                await roleService.RemoveFromRolesAsync(userDTO, removedRoles);

                return RedirectToAction("UserList", "Users");
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}

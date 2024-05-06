﻿using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.Web.Models;
using System.Security.Claims;

namespace MyShopApp.Web.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        ICartService cartService;
        
        public UsersController(IUserService _userService, ICartService _cartService)
        {
            userService = _userService;
            cartService = _cartService;
        }        
        public async Task<IActionResult> UserPage()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userService.GetUserIdAsync(Int32.Parse(userId));
            var cartDTO = await cartService.GetCartAsync(user.CartId);

            var cartLineView = new List<CartLineViewModel>();
            foreach (var cart in cartDTO.CartsLine)
            {
                var cartView = new CartLineViewModel
                {                    
                    MotorcycleName = cart.MotorcycleName,
                    MotorcycleModel = cart.MotorcycleModel,
                    Quantity = cart.Quantity
                };
                cartLineView.Add(cartView);
            }
            var cartViewModel = new CartViewModel
            {
                Id = cartDTO.Id,
                Date = cartDTO.Date,
                cartLineViewModels = cartLineView,
            };
            return View(cartViewModel);
        }    
        public IActionResult UserList() => View(userService.GetUsers());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO { Email = model.Email, Name = model.Email, DateOfBirth = model.DateOfBirth, Password = model.Password };               
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
        public async Task<IActionResult> Edit(int id)
        {
            UserDTO userDTO = await userService.GetUserIdAsync(id);
            if(userDTO == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = userDTO.Id, Email = userDTO.Email, DateOfBirth = userDTO.DateOfBirth };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO? userDTO = await userService.GetUserIdAsync(model.Id);
                if(userDTO!=null)
                {
                    userDTO.Email = model.Email;
                    userDTO.Name = model.Email;
                    userDTO.DateOfBirth = model.DateOfBirth;

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
        public async Task<IActionResult> Delete(int id)
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

        public async Task<IActionResult> ChangePassword(int Id)
        {
            UserDTO userDTO = await userService.GetUserIdAsync(Id);
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
                UserDTO? userDTO = await userService.GetUserIdAsync(model.Id); 
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

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}

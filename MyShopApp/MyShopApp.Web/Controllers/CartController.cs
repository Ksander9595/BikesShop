using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Service;
using MyShopApp.Web.Models;
using System.Security.Claims;

namespace MyShopApp.Web.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;

        public CartController(ICartService cart)
        {
            cartService = cart;
        }

        public IActionResult CartSuccessfully()
        {
            return View();
        }
        public async Task<IActionResult> CartUser()
        {
            var cartDTO = await cartService.GetCartAsync();           

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
                Date = cartDTO.Date,
                cartLineViewModels = cartLineView,
            };
            return View(cartViewModel);
        }

        public async Task<IActionResult> MakeCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                try
                {
                    MotorcycleDTO motorcycleDTO = await cartService.GetMotorcycleAsync(id);

                    CartDTO cartDTO = new CartDTO
                    {
                        MotorcycleId = motorcycleDTO.Id,
                        UserId = Int32.Parse(userId),
                        Date = DateTime.Now,
                    };
                    await cartService.MakeCartAsync(cartDTO);
                    return View("CartSuccessfully");
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}

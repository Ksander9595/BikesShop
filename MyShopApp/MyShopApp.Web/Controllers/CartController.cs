using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.Interfaces;
using MyShopApp.Web.Models;
using System.Security.Claims;

namespace MyShopApp.Web.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;
        IOrderService orderService;

        public CartController(ICartService cart, IOrderService _orderService)
        {
            cartService = cart;
            orderService = _orderService;
        }

        public IActionResult CartSuccessfully()
        {
            return View();
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
                    return RedirectToAction("UserPage", "Users");
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
        [HttpGet]
        public async Task<IActionResult> Checkout(int Id)
        {
            CartDTO cartDTO = await cartService.GetCartAsync(Id);
            if (cartDTO == null)
                return NotFound();
            return View(new CheckoutViewModel { CardId = Id});
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                OrderDTO orderDTO = new OrderDTO
                {
                    NumberCart = model.NumberCart,
                    Validity = model.Validity,
                    CVV = model.CVV,
                    CartId = model.CardId,                   
                };
                await orderService.MakeOrderAsync(orderDTO);
                return RedirectToAction("UserPage", "Users");
            }
            return View(model);
            
        }
    }
}

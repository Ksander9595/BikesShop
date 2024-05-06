using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.Interfaces;
using MyShopApp.Web.Models;
using System.Security.Claims;

namespace MyShopApp.Web.Controllers
{
    public class OrderController : Controller
    {
        IOrderService orderService;

        public OrderController(IOrderService order)
        {
            orderService = order;
        }

        public IActionResult OrderSuccessfully()
        {
            return View();
        }

        public async Task<IActionResult> OrdersList()
        {
            var ordersDTO = await orderService.GetOrdersAsync();
            var ordersView = new List<OrderViewModel>();

            foreach (var order in ordersDTO)
            {
                var cartsView = new List<CartLineViewModel>();
                foreach (var cartLine in order.CartsLineDTO)
                {
                    var cartLineView = new CartLineViewModel
                    {
                        MotorcycleName = cartLine.MotorcycleName,
                        MotorcycleModel = cartLine.MotorcycleModel,
                        Quantity = cartLine.Quantity
                    };
                    cartsView.Add(cartLineView);
                }
                var orderView = new OrderViewModel
                {
                    UserName = order.UserName,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    Zip = order.Zip,
                    cartViewModel = cartsView,
                    Sum = order.Sum,
                    Date = order.Date
                };
                ordersView.Add(orderView);
            }
            return View(ordersView);
        }
        
    }
}

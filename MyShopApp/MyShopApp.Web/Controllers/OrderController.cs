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
                var orderView = new OrderViewModel
                {
                    UserName = order.UserName,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    Zip = order.Zip,
                    //MotorcycleName = order.MotorcycleName,
                    //MotorcycleModel = order.MotorcycleModel,
                    Sum = order.Sum,
                    Date = order.Date
                };
                ordersView.Add(orderView);
            }
            return View(ordersView);
        }      

        public async Task<IActionResult> MakeOrder(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                try
                {
                    //UserDTO userDTO = await userService.GetUserNameAsync(User.Identity.Name);
                    MotorcycleDTO motorcycleDTO = await orderService.GetMotorcycleAsync(id);

                    OrderDTO orderDTO = new OrderDTO
                    {
                        MotorcycleId = motorcycleDTO.Id,
                        UserName = User.Identity.Name,
                        //UserId = userDTO.Id,
                        //Sum = motorcycleDTO.Price,
                        Date = DateTime.Now,
                    };
                    await orderService.MakeOrdersAsync(orderDTO, userId);
                    return View("OrderSuccessfully");
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

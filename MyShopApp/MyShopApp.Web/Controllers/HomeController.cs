using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.DTO;
using AutoMapper;
using MyShopApp.Web.Models;



namespace MyShopApp.Web.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;

        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult Index()
        {          
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MotorcycleDTO, MotorcycleViewModel>()).CreateMapper();
            var motorcycles = mapper.Map<IEnumerable<MotorcycleDTO>, List<MotorcycleViewModel>>(orderService.GetMotorcycles());
            return View(motorcycles);
        }

        public async Task<IActionResult> MakeOrder(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    MotorcycleDTO motorcycle = await orderService.GetMotorcycleAsync(id);
                    var order = new OrderViewModel { MotorcycleID = motorcycle.Id };

                    return View(order);
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);

                }
            }
            else
            {
                return Content("<h2>Add to card need registration</h2>");
            }
        }
        [HttpPost]
        public async Task<IActionResult> MakeOrder(OrderViewModel order)
        {           
                try
                {
                    var orderDto = new OrderDTO
                    {
                        MotorcycleID = order.MotorcycleID,
                        FirstName = order.FirstName,
                        SecondName = order.SecondName,
                        PhoneNumber = order.PhoneNumber,
                        Address = order.Address,
                        Date = order.Date
                    };
                    await orderService.MakeOrder(orderDto);
                    return Content("<h2>Your order successfully completed<h2>");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return View(order);           
        }
        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }


    }
}

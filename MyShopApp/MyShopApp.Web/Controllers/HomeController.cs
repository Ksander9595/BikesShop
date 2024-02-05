using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.Service;
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
            IEnumerable<MotocycleDTO> motocyclesDtos = orderService.GetMotocycles();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MotocycleDTO, MotocycleViewModel>()).CreateMapper();
            var motocycles = mapper.Map<IEnumerable<MotocycleDTO>, List<MotocycleViewModel>>(motocyclesDtos);
            return View(motocycles);
        }

        public IActionResult MakeOrder(int? id)
        {
            try
            {
                MotocycleDTO motocycle = orderService.GetMotocycle(id);
                var order = new OrderViewModel { MotocycleID = motocycle.Id };

                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);

            }
        }
        [HttpPost]
        public IActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                var orderDto = new OrderDTO
                {
                    MotocycleID = order.MotocycleID,
                    FirstName = order.FirstName,
                    SecondName = order.SecondName,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    Date = order.Date
                };
                orderService.MakeOrder(orderDto);
                return Content("<h2>Your order successfully completed<h2>");
            }
            catch(ValidationException ex)
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

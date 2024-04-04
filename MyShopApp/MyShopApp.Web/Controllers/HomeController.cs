using Microsoft.AspNetCore.Mvc;
using MyShopApp.BLL.Interfaces;
using MyShopApp.BLL.DTO;
using AutoMapper;
using MyShopApp.Web.Models;



namespace MyShopApp.Web.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;

        public HomeController(IOrderService order)
        {
            orderService = order;
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

        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }



    }
}

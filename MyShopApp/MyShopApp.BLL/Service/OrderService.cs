using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using AutoMapper;


namespace MyShopApp.BLL.Service
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database;

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;

        }

        public async Task MakeOrderAsync(OrderDTO orderDto)
        {
            var carts = await Database.Carts.GetAll();
            var cartUser = carts.Where(c => c.Id == orderDto.CartId).FirstOrDefault();
            var user = await Database.UserManager.FindByIdAsync(cartUser.UserId.ToString());
            Order order = new Order
            {
                Date = DateTime.Now,
                cart = cartUser,               
                Sum = orderDto.Sum,
                NumberCart = orderDto.NumberCart, 
                Validity = orderDto.Validity, 
                CVV = orderDto.CVV 
            };
            await Database.Orders.CreateAsync(order);
            await Database.SaveAsync();
        }

        public async Task<IEnumerable<MotorcycleDTO>> GetMotorcycles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Motorcycle, MotorcycleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Motorcycle>, List<MotorcycleDTO>>(await Database.Motorcycles.GetAll());
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await Database.Orders.GetAll();           
            var ordersDTO = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var cartsLineDTO = new List<CartLineDTO>();
                foreach(var cartLine in order.cart.CartLine)
                {
                    var moto = await Database.Motorcycles.GetAsync(cartLine.MotorcycleId);
                    var cartLineDTO = new CartLineDTO
                    {
                        MotorcycleName = moto.Name,
                        MotorcycleModel = moto.Model,
                        Quantity = cartLine.Quantity
                    };
                    cartsLineDTO.Add(cartLineDTO);
                }
                var user = await Database.UserManager.FindByIdAsync(order.cart.UserId.ToString());
                var orderDTO = new OrderDTO
                {
                    Id = order.Id,
                    Date = order.Date,
                    UserName = user.UserName,
                    CartsLineDTO = cartsLineDTO,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Zip = user.Zip,
                    NumberCart = order.NumberCart,
                    Validity = order.Validity,
                    CVV = order.CVV
                };
                ordersDTO.Add(orderDTO);
            }
            return ordersDTO;
        }
     
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

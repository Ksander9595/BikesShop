using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using AutoMapper;
using System.Diagnostics.Eventing.Reader;


namespace MyShopApp.BLL.Service
{
    public class OrderService : IOrderService//GetMotorcyclesAsync
    {
        IUnitOfWork Database;       

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
            
        }

        public async Task MakeOrderAsync(OrderDTO orderDto)
        {
            var ordersUsers = await Database.Orders.GetAll();
            var orderUser = ordersUsers.Where(u => u.UserId == orderDto.UserId).FirstOrDefault();
            var user = await Database.UserManager.FindByIdAsync(orderDto.UserId.ToString());
            var moto = await Database.Motorcycles.GetAsync(orderDto.MotorcycleId);
            if (orderUser == null)
            {
                
                Order order = new Order
                {
                    User = user,
                    Date = orderDto.Date,
                };
                await Database.Orders.CreateAsync(order);
                await Database.SaveAsync();
                Cart cart = new Cart
                {
                    motorcycle = moto,
                    order = order,
                    Quantity = 1
                };
                await Database.Carts.CreateAsync(cart);
                await Database.SaveAsync();
            }
            else
            {
                var cartUser = orderUser.Cart.Where(m => m.MotorcycleId == orderDto.MotorcycleId).FirstOrDefault();
                if (cartUser == null)                
                {
                    Cart cart = new Cart
                    {
                        OrderId = user.Order.Id,
                        motorcycle = moto,
                        Quantity = 1,
                    };
                    await Database.Carts.CreateAsync(cart);
                    await Database.SaveAsync();
                }
                else
                {
                    var cart = orderUser.Cart.Where(m => m.MotorcycleId == orderDto.MotorcycleId).FirstOrDefault();                    
                    cart.Quantity += 1;
                    Database.Carts.Update(cart);
                    await Database.SaveAsync();
                }
            }                     
        }       

        public async Task<IEnumerable<MotorcycleDTO>> GetMotorcycles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Motorcycle, MotorcycleDTO>()).CreateMapper();          
            return mapper.Map<IEnumerable<Motorcycle>, List<MotorcycleDTO>>(await Database.Motorcycles.GetAll());
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()     
        {                     
            var ordersDTO = new List<OrderDTO>();
            foreach(var order in await Database.Orders.GetAll()) 
            {
                var user = await Database.UserManager.FindByIdAsync(order.UserId.ToString());
                var cartsDTO = new List<CartDTO>();
                foreach(var cart in order.Cart)
                {
                    var cartDTO = new CartDTO
                    {
                        MotorcycleName = cart.motorcycle.Name,
                        MotorcycleModel = cart.motorcycle.Model,
                        Quantity = cart.Quantity,

                    };
                    cartsDTO.Add(cartDTO);
                }
                var orderDTO = new OrderDTO
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Zip = user.Zip,
                    cartDTOs = cartsDTO,
                    Sum = order.Sum,
                    Date = order.Date,                   
                };
                ordersDTO.Add(orderDTO);
            }
            return ordersDTO;
        }

        public async Task<MotorcycleDTO> GetMotorcycleAsync(int id)
        {           
            if (id == null)
                throw new ValidationException("ID motorcycle not found", "");
            var motorcycle = await Database.Motorcycles.GetAsync(id);
            if (motorcycle == null)
                throw new ValidationException("Motorcycle not found", "");

            return new MotorcycleDTO
            {
                Id = motorcycle.Id,
                Name = motorcycle.Name,
                Model = motorcycle.Model,
                Description = motorcycle.Description,
                Price = motorcycle.Price,
                motoClass = motorcycle.motoClass,
                Year = motorcycle.Year,
                Hp = motorcycle.Hp,
                Capacity = motorcycle.Capacity,
                Document = motorcycle.Document,
                Mileage = motorcycle.Mileage,
                Color = motorcycle.Color,
                Condition = motorcycle.Condition,
                Availability = motorcycle.Availability
            };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

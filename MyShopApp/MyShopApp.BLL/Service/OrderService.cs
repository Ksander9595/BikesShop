using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using AutoMapper;


namespace MyShopApp.BLL.Service
{
    public class OrderService : IOrderService//GetMotorcyclesAsync
    {
        IUnitOfWork Database;       

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
            
        }

        public async Task MakeOrdersAsync(OrderDTO orderDto, string id)
        {
            User? user = await Database.UserManager.FindByIdAsync(id);
            var moto = await Database.Motorcycles.GetAsync(orderDto.MotorcycleId);
            if (user != null)
            {
                if (user.Order != null)
                {
                    var cartLine = user.Order.Cart.Where(m => m.motorcycle.Id == moto.Id).FirstOrDefault();

                    if (cartLine != null)
                    {
                        cartLine.Quantity += 1;
                    }
                    else
                    {
                        user.Order.Cart.Add(new CartLine { MotorcycleId = moto.Id, Quantity = 1 });
                    }
                }
                else
                {
                    var order = new Order
                    {
                        UserId = user.Id,
                        Date = orderDto.Date,
                        Cart = new List<CartLine> { new CartLine { MotorcycleId = moto.Id, Quantity = 1 } },
                        Sum = moto.Price
                    };
                    await Database.Orders.CreateAsync(order);                   
                }
                await Database.SaveAsync();
            }           
        }

        public async Task MakeOrderAsync(OrderDTO orderDto)
        {
            var motorcycle = await Database.Motorcycles.GetAsync(orderDto.MotorcycleId);            
           
            if (motorcycle == null)
            {
                throw new ValidationException("Motorcycle not found", "");
            }
            
            Order order = new Order
            {
                
                UserId = orderDto.UserId,
                Date = orderDto.Date,
                //MotorcycleId = motorcycle.Id,
                Sum = orderDto.Sum,
            };
            await Database.Orders.CreateAsync(order);
            await Database.SaveAsync();
        }

        public IEnumerable<MotorcycleDTO> GetMotorcycles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Motorcycle, MotorcycleDTO>()).CreateMapper();          
            return mapper.Map<IEnumerable<Motorcycle>, List<MotorcycleDTO>>(Database.Motorcycles.GetAll());
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()//отношение многие ко многим?
        {                     
            var ordersDTO = new List<OrderDTO>();
            foreach(var order in Database.Orders.GetAll()) 
            {
                var user = await Database.UserManager.FindByIdAsync(order.UserId.ToString());
                //var motorcycle = await Database.Motorcycles.GetAsync(order.Id);
                var cartLineDTO = new List<CartLineDTO>();
                foreach (var cartList in order.Cart)
                {
                    var cartLine = new CartLineDTO
                    {
                        MotorcycleId = cartList.MotorcycleId,
                        Quantity = cartList.Quantity
                    };
                    cartLineDTO.Add(cartLine);
                }
                var orderDTO = new OrderDTO
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Zip = user.Zip,
                    //MotorcycleName = motorcycle.Name,
                    //MotorcycleModel = motorcycle.Model,
                    Sum = order.Sum,
                    Date = order.Date,
                    cartLineDTO = cartLineDTO
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

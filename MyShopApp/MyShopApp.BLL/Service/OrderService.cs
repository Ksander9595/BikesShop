
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
        IidentityUnitOfWork IdentityDatabase;//????

        public OrderService(IUnitOfWork uow, IidentityUnitOfWork iuow)
        {
            Database = uow;
            IdentityDatabase = iuow;
        }
        public async Task MakeOrderAsync(OrderDTO orderDto)
        {
            var motorcycle = await Database.Motorcycles.GetAsync(orderDto.ProductId);            
           
            if (motorcycle == null)
            {
                throw new ValidationException("Motorcycle not found", "");
            }
            
            Order order = new Order
            {
                
                UserId = orderDto.UserId,
                Date = orderDto.Date,
                MotorcycleId = motorcycle.Id,
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
                var user = await IdentityDatabase.UserManager.FindByIdAsync(order.UserId.ToString());
                var motorcycle = await Database.Motorcycles.GetAsync(order.MotorcycleId);
                var orderDTO = new OrderDTO
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Zip = user.Zip,
                    MotorcycleName = motorcycle.Name,
                    MotorcycleModel = motorcycle.Model,
                    Sum = order.Sum,
                    Date = order.Date
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

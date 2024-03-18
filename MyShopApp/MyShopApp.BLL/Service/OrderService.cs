
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.BusinessModel;
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
        public async Task MakeOrder(OrderDTO orderDto)
        {
            var motorcycle = await Database.Motorcycles.GetAsync(orderDto.MotorcycleID);            
           
            if (motorcycle == null)
            {
                throw new ValidationException("Motorcycle not found", "");
            }
            decimal sum = new Discount(0.1m).GetDiscountedPrice(motorcycle.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                MotorcycleId = motorcycle.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            await Database.Orders.CreateAsync(order);
            await Database.SaveAsync();
        }

        public IEnumerable<MotorcycleDTO> GetMotorcycles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Motorcycle, MotorcycleDTO>()).CreateMapper();          
            return mapper.Map<IEnumerable<Motorcycle>, List<MotorcycleDTO>>(Database.Motorcycles.GetAll());
        }

        public async Task<MotorcycleDTO> GetMotorcycleAsync(int? id)
        {           
            if (id == null)
                throw new ValidationException("ID motorcycle not found", "");
            var motorcycle = await Database.Motorcycles.GetAsync(id.Value);
            if (motorcycle == null)
                throw new ValidationException("Motorcycle not found", "");

            return new MotorcycleDTO
            {
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

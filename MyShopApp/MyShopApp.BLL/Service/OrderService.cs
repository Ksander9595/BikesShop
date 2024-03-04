
using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.BusinessModel;
using AutoMapper;


namespace MyShopApp.BLL.Service
{
    public class OrderService : IOrderService//async-await
    {
        IUnitOfWork Database;

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            Motorcycle motorcycle = Database.Motorcycles.GetAsync(orderDto.MotocycleID);
           
            if (motorcycle == null)
            {
                throw new ValidationException("Motocycle not found", "");
            }
            decimal sum = new Discount(0.1m).GetDiscountedPrice(motorcycle.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                MotocycleId = motorcycle.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public IEnumerable<MotorcycleDTO> GetMotocycles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Motorcycle, MotorcycleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Motorcycle>, List<MotorcycleDTO>>(Database.Motorcycles.GetAll());
        }

        public MotorcycleDTO GetMotocycle(int? id)
        {
            if (id == null)
                throw new ValidationException("ID motocycle not found", "");
            var motocycle = Database.Motorcycles.Get(id.Value);
            if (motocycle == null)
                throw new ValidationException("Motocycle not found", "");

            return new MotorcycleDTO
            {
                Name = motocycle.Name,
                Model = motocycle.Model,
                Description = motocycle.Description,
                Price = motocycle.Price,
                motoClass = motocycle.motoClass,
                Year = motocycle.Year,
                Hp = motocycle.Hp,
                Capacity = motocycle.Capacity,
                Document = motocycle.Document,
                Mileage = motocycle.Mileage,
                Color = motocycle.Color,
                Condition = motocycle.Condition,
                Availability = motocycle.Availability
            };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

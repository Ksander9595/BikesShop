using MyShopApp.BLL.DTO;
using MyShopApp.BLL.Infrastructure;
using MyShopApp.BLL.Interfaces;
using MyShopApp.DAL.EF.Entities;
using MyShopApp.DAL.Interfaces;
using System.Security.Claims;

namespace MyShopApp.BLL.Service
{
    public class CartService : ICartService
    {
        IUnitOfWork Database;

        public CartService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task MakeCartAsync(CartDTO cartDto)
        {
            var cartsUsers = await Database.Carts.GetAll();
            var cartUser = cartsUsers.Where(u => u.UserId == cartDto.UserId).FirstOrDefault();
            var user = await Database.UserManager.FindByIdAsync(cartDto.UserId.ToString());
            var moto = await Database.Motorcycles.GetAsync(cartDto.MotorcycleId);
            if (cartUser == null)
            {

                Cart cart = new Cart
                {
                    user = user,
                    Date = cartDto.Date,
                };
                await Database.Carts.CreateAsync(cart);
                await Database.SaveAsync();
                CartLine cartLine = new CartLine
                {
                    motorcycle = moto,
                    Cart = cart,
                    Quantity = 1
                };
                await Database.CartsLine.CreateAsync(cartLine);
                await Database.SaveAsync();
            }
            else
            {
                var cartLineUser = cartUser.CartLine.Where(m => m.MotorcycleId == cartDto.MotorcycleId).FirstOrDefault();
                if (cartLineUser == null)
                {
                    CartLine cartLine = new CartLine
                    {
                        CartId = user.Cart.Id,
                        motorcycle = moto,
                        Quantity = 1,
                    };
                    await Database.CartsLine.CreateAsync(cartLine);
                    await Database.SaveAsync();
                }
                else
                {
                    var cartLineUSer = cartUser.CartLine.Where(m => m.MotorcycleId == cartDto.MotorcycleId).FirstOrDefault();
                    cartLineUser.Quantity += 1;
                    Database.CartsLine.Update(cartLineUser);
                    await Database.SaveAsync();
                }
            }
        }

        public async Task <CartDTO> GetCartAsync(int Id)
        {                    
            var carts = await Database.Carts.GetAll();
            var cartUser = carts.Where(c=>c.Id == Id).FirstOrDefault();
            var cartsLineDTO = new List<CartLineDTO>();           
            foreach (var cartLine in cartUser.CartLine)
            {
                var cartLineDTO = new CartLineDTO
                {
                    MotorcycleName = cartLine.motorcycle.Name,
                    MotorcycleModel = cartLine.motorcycle.Model,
                    Quantity = cartLine.Quantity,

                };
                cartsLineDTO.Add(cartLineDTO);
            }
            var cartDTO = new CartDTO
            {
                Id = Id,
                Date = cartUser.Date,
                CartsLine = cartsLineDTO
            };                            
                return cartDTO;
        }
        public async Task<MotorcycleDTO> GetMotorcycleAsync(int Id)
        {
            if (Id == 0)
                throw new ValidationException("ID motorcycle not found", "");
            var motorcycle = await Database.Motorcycles.GetAsync(Id);
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

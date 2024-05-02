using MyShopApp.BLL.DTO;
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

        public async Task<IEnumerable<CartDTO>> GetCartsUserAsync()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartDTO = new CartDTO();
            var carts = await Database.Carts.GetAll();
            var cartUser = carts.Where(c=>c.UserId == Int32.Parse(userId)).FirstOrDefault();
                var user = await Database.UserManager.FindByIdAsync(order.UserId.ToString());
                var cartsDTO = new List<CartDTO>();
                foreach (var cart in order.Cart)
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
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

using MyShopApp.BLL.DTO;


namespace MyShopApp.BLL.Interfaces
{
    public interface ICartService : IDisposable
    {
        Task MakeCartAsync(CartDTO cartDto);
        Task<CartDTO> GetCartAsync();
    }
}

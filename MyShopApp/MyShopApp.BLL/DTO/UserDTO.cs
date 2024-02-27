

namespace MyShopApp.BLL.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }     
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
        public int Year { get; set; }
    }
}

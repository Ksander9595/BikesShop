

using Microsoft.VisualBasic;

namespace MyShopApp.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        //public string? Role { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Zip { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

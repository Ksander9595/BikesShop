﻿namespace MyShopApp.Web.Models
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
    }
}

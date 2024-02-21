
using MyShopApp.BLL.DTO;

namespace MyShopApp.Web.Models
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<RoleDTO> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<RoleDTO>();
            UserRoles = new List<string>(); 
        }
    }
}

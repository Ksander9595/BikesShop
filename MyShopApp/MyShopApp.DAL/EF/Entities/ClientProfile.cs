using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopApp.DAL.EF.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string? Id {  get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public virtual User? User { get; set; }
    }
}

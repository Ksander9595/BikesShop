using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyShopApp.DAL.EF.Entities;


namespace MyShopApp.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        public ApplicationRoleManager(IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger) { }
    }
}

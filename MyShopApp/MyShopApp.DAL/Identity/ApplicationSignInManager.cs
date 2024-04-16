using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyShopApp.DAL.EF.Entities;

namespace MyShopApp.DAL.Identity
{
    public class ApplicationSignInManager: SignInManager<User>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<User> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<User>> logger, 
            IAuthenticationSchemeProvider schemes
            //IUserConfirmation<User> confirmation
            )
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes) { }
    }
}

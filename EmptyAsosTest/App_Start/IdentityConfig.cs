using EmptyAsosTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using Microsoft.Owin.Security;

namespace EmptyAsosTest
{
    public class IdentityHubUserManager : UserManager<IdentityHubUser>
    {
        public IdentityHubUserManager(IUserStore<IdentityHubUser> store) : base(store)
        {
        }

        public static IdentityHubUserManager Create(IdentityFactoryOptions<IdentityHubUserManager> options, IOwinContext context)
        {
            var manager = new IdentityHubUserManager(new UserStore<IdentityHubUser>(context.Get<ApplicationDbContext>()));
            
            manager.UserValidator = new UserValidator<IdentityHubUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }

    public class IdentityHubSignInManager : SignInManager<IdentityHubUser, string>
    {
        public IdentityHubSignInManager(UserManager<IdentityHubUser, string> userManager, IAuthenticationManager authenticationManager) 
            : base(userManager, authenticationManager)
        {
        }


        public static IdentityHubSignInManager Create(IdentityFactoryOptions<IdentityHubSignInManager> options, IOwinContext context)
        {
            return new IdentityHubSignInManager(context.GetUserManager<IdentityHubUserManager>(), context.Authentication);
        }
    }
}
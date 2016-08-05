using EmptyAsosTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EmptyAsosTest
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<IdentityHubUserManager>(IdentityHubUserManager.Create);
            app.CreatePerOwinContext<IdentityHubSignInManager>(IdentityHubSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityHubUserManager, IdentityHubUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(Paths.TokenPath),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
#if DEBUG
                AllowInsecureHttp = true,
#endif
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredential,
                    OnValidateTokenRequest = ValidateTokenRequest,
                    OnValidateClientAuthentication = ValidateClientAuthentication
                }
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext arg)
        {
            string clientId;
            string clientSecret;
            if (arg.TryGetBasicCredentials(out clientId, out clientSecret) ||
                arg.TryGetFormCredentials(out clientId, out clientSecret))
            {
                arg.Validated();
            }
            return Task.FromResult(0);
        }

        private Task ValidateTokenRequest(OAuthValidateTokenRequestContext arg)
        {
            arg.Validated();
            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredential(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("urn:oauth:scope", x)).Union(new List<Claim> { new Claim("MenulogAppType","true",ClaimValueTypes.Boolean,ClaimsIdentity.DefaultIssuer) }));

            context.Validated(identity);

            return Task.FromResult(0);
        }
    }
}
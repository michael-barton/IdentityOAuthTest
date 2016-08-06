using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ResourceServer
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions()
            {
                //Provider = new OAuthProvider()
            });
        }
    }

    public class OAuthProvider : IOAuthBearerAuthenticationProvider
    {
        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return Task.FromResult(0);
        }

        public Task RequestToken(OAuthRequestTokenContext context)
        {
            return Task.FromResult(0);
        }

        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            return Task.FromResult(0);
        }
    }
}
using Owin;
using System;
using Microsoft.Owin.Cors;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions()
            {                
            });
        }
    }
}
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(ResourceServer.Startup))]
namespace ResourceServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();

            ConfigureAuth(app);

            //WebApiConfig.Register(config);
            //app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(config);
        }
    }
}
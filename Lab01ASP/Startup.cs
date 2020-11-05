using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab01ASP.Startup))]
namespace Lab01ASP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

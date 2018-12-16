using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CongTyGachMen.Startup))]
namespace CongTyGachMen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

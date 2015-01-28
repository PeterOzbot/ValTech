using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KDTreeWeb.Startup))]
namespace KDTreeWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeakDetectionSample.Startup))]
namespace PeakDetectionSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

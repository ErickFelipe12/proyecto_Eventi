using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventiApp.Startup))]
namespace EventiApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

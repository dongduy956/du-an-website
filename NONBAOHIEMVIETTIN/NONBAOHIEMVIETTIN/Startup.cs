using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NONBAOHIEMVIETTIN.Startup))]
namespace NONBAOHIEMVIETTIN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}

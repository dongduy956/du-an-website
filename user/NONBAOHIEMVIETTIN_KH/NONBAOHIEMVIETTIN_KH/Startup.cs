using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NONBAOHIEMVIETTIN_KH.Startup))]
namespace NONBAOHIEMVIETTIN_KH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

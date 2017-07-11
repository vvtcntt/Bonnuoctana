using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TANA.Startup))]
namespace TANA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

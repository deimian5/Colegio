using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Colegio.Startup))]
namespace Colegio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

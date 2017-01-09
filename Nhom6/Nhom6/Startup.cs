using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nhom6.Startup))]
namespace Nhom6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

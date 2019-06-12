using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppManagement.Startup))]
namespace AppManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

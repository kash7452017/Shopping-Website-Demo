using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Test.Startup))]
namespace MVC_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

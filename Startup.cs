using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LittleFish3._0.Startup))]
namespace LittleFish3._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Database_1.Startup))]
namespace Database_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

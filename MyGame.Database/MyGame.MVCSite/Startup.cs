using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyGame.MVCSite.Startup))]
namespace MyGame.MVCSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(selfCoding.Startup))]
namespace selfCoding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

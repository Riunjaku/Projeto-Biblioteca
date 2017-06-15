using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assessment.Web.Startup))]
namespace Assessment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

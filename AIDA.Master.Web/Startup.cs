using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AIDA.Master.Web.Startup))]
namespace AIDA.Master.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

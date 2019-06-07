using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalMerchBuild.Startup))]
namespace FinalMerchBuild
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

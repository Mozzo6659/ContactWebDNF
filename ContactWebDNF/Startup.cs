using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactWebDNF.Startup))]
namespace ContactWebDNF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

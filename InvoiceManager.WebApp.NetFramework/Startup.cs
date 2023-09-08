using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceManager.WebApp.NetFramework.Startup))]
namespace InvoiceManager.WebApp.NetFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

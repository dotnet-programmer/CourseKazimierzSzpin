using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendEmail.NetFramework.WebApp.Startup))]

namespace SendEmail.NetFramework.WebApp
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app) => ConfigureAuth(app);
	}
}
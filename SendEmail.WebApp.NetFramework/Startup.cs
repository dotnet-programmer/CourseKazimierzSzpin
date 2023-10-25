using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendEmail.WebApp.NetFramework.Startup))]

namespace SendEmail.WebApp.NetFramework
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app) => ConfigureAuth(app);
	}
}
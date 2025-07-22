using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceManager.NetFramework.WebApp.Startup))]

namespace InvoiceManager.NetFramework.WebApp
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app) => ConfigureAuth(app);
	}
}

// Szablon aplikacji: ASP.NET Web Application (.NET Framework)
// MVC + Authentication - Individual User Accounts + HTTPS
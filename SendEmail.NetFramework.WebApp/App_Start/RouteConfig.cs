using System.Web.Mvc;
using System.Web.Routing;

namespace SendEmail.NetFramework.WebApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "AccountRoute",
				url: "Account/{action}/{id}",
				defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Default",
				url: "{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);


		}
	}
}
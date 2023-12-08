using System.Web.Mvc;
using System.Web.Routing;

namespace InvoiceManager.NetFramework.WebApp
{
	// INFO - ta klasa umożliwia zdefiniowanie jak działa routing aplikacji,
	// jak wyglądają adresy URL i jakie akcje mają być wywoływane
	// na podstawie tego co wpisze użytkownik w przeglądarce można ustawić jaka akcja z którego kontrolera zostanie wywołana
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// dodatkowy routing
			//routes.MapRoute(
			//	name: "MyRoute",
			//	url: "test/{action}/{controller}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
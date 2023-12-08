using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InvoiceManager.NetFramework.WebApp
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		// INFO - Ustawienia regionalne - strona serwera
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			CultureInfo cultureInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
			cultureInfo.DateTimeFormat.DateSeparator = "-";
			cultureInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
			cultureInfo.NumberFormat.NumberDecimalDigits = 2;
			cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
			cultureInfo.NumberFormat.NumberGroupSeparator = "";
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}
	}
}
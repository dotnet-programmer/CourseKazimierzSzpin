using System.Web.Optimization;

namespace InvoiceManager.NetFramework.WebApp
{
	// INFO - wiązanie kilku plików .js i .css w 1 plik, dzięki temu kod ładuje się szybciej i łatwiej odwołać się do niego
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js", 
				"~/Scripts/jquery.cookie.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			bundles.Add(new Bundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.js", 
				"~/Scripts/bootstrap-datepicker.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/bootstrap.css", 
				"~/Content/bootstrap-datepicker.css", 
				"~/Content/bootstrap-datepicker3.css", 
				"~/Content/site.css"));
		}
	}
}
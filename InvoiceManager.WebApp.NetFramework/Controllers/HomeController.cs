using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.WebApp.NetFramework.Controllers
{
	// INFO - metody kontrolera nazywa się akcjami, odpowiadają za wyświetlenie odpowiedniego widoku
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			string test = "testy 123";

			return View((object)test);
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
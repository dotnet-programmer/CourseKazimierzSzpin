using System.Diagnostics;
using System.Web.Mvc;

namespace InvoiceManager.NetFramework.WebApp.Filters
{
	// INFO - własna klasa atrybutu Action Filter
	public class TimerAttribute : ActionFilterAttribute
	{
		private Stopwatch _stopWatch;

		// Powinniśmy być ostatnim filtrem – tuż przed akcją
		public TimerAttribute()
			=>	Order = int.MaxValue;

		// zawsze wywoływana przed wywołaniem określonej akcji na której został zastosowany atrybut
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = filterContext.Controller;
			if (controller != null)
			{
				//var stopwatch = new Stopwatch();
				//_stopWatch = stopwatch;
				//stopwatch.Start();

				_stopWatch = Stopwatch.StartNew();
			}
		}

		// zawsze wywoływana zaraz po wywołaniu określonej akcji na której został zastosowany atrybut
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var controller = filterContext.Controller;
			if (controller != null)
			{
				//var stopwatch = (Stopwatch)_stopWatch;
				//stopwatch.Stop();
				//controller.ViewBag._Duration = stopwatch.Elapsed.TotalMilliseconds;

				_stopWatch.Stop();
				controller.ViewBag.Duration = _stopWatch.Elapsed.TotalMilliseconds;
			}
		}
	}
}
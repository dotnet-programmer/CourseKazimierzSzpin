using System.Web.Mvc;

namespace InvoiceManager.WebApp.NetFramework
{
	// INFO - umożliwia dodanie nowego filtra który będzie wywoływany przed każdym requestem
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new CustomExceptionFilter());
		}

		public class CustomExceptionFilter : IExceptionFilter
		{
			public void OnException(ExceptionContext filterContext)
			{
				var exception = filterContext.Exception;
				// logowanie do pliku
			}
		}
	}
}
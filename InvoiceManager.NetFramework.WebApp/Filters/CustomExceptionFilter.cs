using System.Web.Mvc;

namespace InvoiceManager.NetFramework.WebApp.Filters
{
	public class CustomExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			var exception = filterContext.Exception;
			// logowanie do pliku
		}
	}
}
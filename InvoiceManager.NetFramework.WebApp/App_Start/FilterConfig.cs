using System.Web.Mvc;
using InvoiceManager.NetFramework.WebApp.Filters;

namespace InvoiceManager.NetFramework.WebApp
{
	// INFO - umożliwia dodanie nowego filtra który będzie wywoływany przed każdym requestem
	public partial class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new CustomExceptionFilter());

			// INFO - własna klasa atrybutu Action Filter zastosowana dla wszystkich akcji we wszystkich kontrolerach
			filters.Add(new TimerAttribute());
		}
	}
}
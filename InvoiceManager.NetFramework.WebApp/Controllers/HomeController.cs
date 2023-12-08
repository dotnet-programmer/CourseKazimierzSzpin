using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using InvoiceManager.NetFramework.WebApp.ActionResults;
using InvoiceManager.NetFramework.WebApp.Filters;
using InvoiceManager.NetFramework.WebApp.Models.Domains;
using InvoiceManager.NetFramework.WebApp.Models.Repositories;
using InvoiceManager.NetFramework.WebApp.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace InvoiceManager.NetFramework.WebApp.Controllers
{
	// INFO - metody kontrolera nazywa się akcjami, odpowiadają za wyświetlenie odpowiedniego widoku

	[Authorize]
	public class HomeController : Controller
	{
		private readonly InvoiceRepository _invoiceRepository = new InvoiceRepository();
		private readonly ClientRepository _clientRepository = new ClientRepository();
		private readonly ProductRepository _productRepository = new ProductRepository();

		private static int _number = 0;

		#region Actions

		// umożliwia wykonanie akcji niezalogowanym użytkownikom
		[AllowAnonymous]

		// INFO - własna klasa atrybutu Action Filter
		[Timer]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			// przekazanie danych jako model z kontrolera do widoku
			string test = "testy 123";

			ViewBag.MyNumber = test;

			// INFO - Sesja
			// pobranie wartości sesji
			int sessionValue = GetSession();
			// zwiększenie wartości sesji
			sessionValue++;
			// aktualizacja wartości sesji
			UpdateSession(sessionValue);
			// wyświetlenie wartości sesji w widoku za pomocą ViewBag
			ViewBag.SessionValue = sessionValue;

			// INFO - Cookie
			// pobranie wartości cookie
			int cookieValue = GetCookie();
			// zwiększenie wartości cookie
			cookieValue++;
			// aktualizacja wartości cookie
			UpdateCookie(cookieValue);
			// wyświetlenie wartości cookie w widoku za pomocą ViewBag
			ViewBag.CookieValue = cookieValue;

			// INFO - Cache
			// pobranie wartości cache
			int cacheValue = GetCache();
			// zwiększenie wartości cache
			cacheValue++;
			// aktualizacja wartości cache
			UpdateCache(cacheValue);
			// wyświetlenie wartości cache w widoku za pomocą ViewBag
			ViewBag.CacheValue = cacheValue;

			return View((object)test);
		}

		[AllowAnonymous]

		// INFO - Cache, czas trwania danych w cache ustawiony na 10s
		[OutputCache(Duration = 10)]
		public ActionResult Contact()
		{
			_number++;

			ViewBag.Message = "Your contact page. =>" + _number;

			// zmiana domyślnego widoku zwraacnego przez kontroler - trzeba podać nazwę widoku który ma zostać wyświetlony
			//return View("About");

			return View();
		}

		public ActionResult Index() => View(_invoiceRepository.GetInvoices(GetUserId()));

		//jeśli id = 0 to dodawanie nowej faktury, a jak jest jakieś id to będzie edycja
		public ActionResult Invoice(int invoiceId = 0)
		{
			var userId = GetUserId();
			Invoice invoice = (invoiceId == 0) ? GetNewInvoice(userId) : _invoiceRepository.GetInvoice(invoiceId, userId);
			EditInvoiceViewModel viewModel = PrepareEditInvoiceViewModel(invoice, userId);
			return View(viewModel);
		}

		// oznaczenie metody typu Post
		[HttpPost]

		// INFO - zabezpieczenie przed atakiem Cross-site request forgery (w skrócie CSRF lub XSRF)
		[ValidateAntiForgeryToken]

		// parametr używany przez formularz, który dopasowuje nazwy inputów do nazw właściwości obiektu i przypisuje im odpowiednie wartości
		public ActionResult Invoice(Invoice invoice)
		{
			var userId = GetUserId();
			invoice.UserId = userId;

			// INFO - walidacja poprawności wprowadzonych danych
			// ModelState.IsValid sprawdza poprawność pól na podstawie danych wpisanych w atrybutach modeli, np czy jest wymagany, jaka długość itp.
			if (!ModelState.IsValid)
			{
				EditInvoiceViewModel viewModel = PrepareEditInvoiceViewModel(invoice, userId);
				return View("Invoice", viewModel);
			}

			if (invoice.Id == 0)
			{
				_invoiceRepository.Add(invoice);
			}
			else
			{
				_invoiceRepository.Update(invoice);
			}
			return RedirectToAction("Index");
		}

		// INFO - dodanie własnej strony błędu wyświetlanej podczas wystąpienia określonego typu wyjątku
		[HandleError(ExceptionType = typeof(ArgumentException), View = "ArgumentException")]
		public ActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
		{
			// INFO - dodanie własnej strony błędu wyświetlanej podczas wystąpienia określonego typu wyjątku
			if (invoicePositionId < 0)
			{
				throw new ArgumentException(nameof(invoicePositionId));
			}

			var userId = GetUserId();
			InvoicePosition invoicePosition = (invoicePositionId == 0) ? GetNewInvoicePosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);
			EditInvoicePositionViewModel viewModel = PrepareEditInvoicePositionViewModel(invoicePosition);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult InvoicePosition(InvoicePosition invoicePosition)
		{
			if (!ModelState.IsValid)
			{
				var vm = PrepareEditInvoicePositionViewModel(invoicePosition);
				return View("InvoicePosition", vm);
			}

			Product product = _productRepository.GetProduct(invoicePosition.ProductId);
			invoicePosition.Value = invoicePosition.Quantity + product.Value;

			var userId = GetUserId();
			if (invoicePosition.Id == 0)
			{
				_invoiceRepository.AddPosition(invoicePosition, userId);
			}
			else
			{
				_invoiceRepository.UpdatePosition(invoicePosition, userId);
			}
			_invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);
			return RedirectToAction("Invoice", new { invoiceId = invoicePosition.InvoiceId });
		}

		// ta metoda zwraca JSON bo jest wywoływana przez Ajax
		[HttpPost]
		public ActionResult Delete(int invoiceId)
		{
			try
			{
				var userId = GetUserId();
				_invoiceRepository.Delete(invoiceId, userId);
			}
			catch (Exception ex)
			{
				// logowanie błędów
				return Json(new { Success = false, Message = ex.Message });
			}
			return Json(new { Success = true });
		}

		// ta metoda zwraca JSON bo jest wywoływana przez Ajax
		[HttpPost]
		public ActionResult DeletePosition(int positionId, int invoiceId)
		{
			decimal invoiceValue;
			try
			{
				var userId = GetUserId();
				_invoiceRepository.DeletePosition(positionId, userId);
				invoiceValue = _invoiceRepository.UpdateInvoiceValue(invoiceId, userId);
			}
			catch (Exception ex)
			{
				// logowanie błędów
				return Json(new { Success = false, Message = ex.Message });
			}
			return Json(new { Success = true, InvoiceValue = invoiceValue });
		}

		#endregion Actions

		#region Private methods

		private string GetUserId() => User.Identity.GetUserId();

		private Invoice GetNewInvoice(string userId)
			=> new Invoice
			{
				UserId = userId,
				CreatedDate = DateTime.Now,
				PaymentDate = DateTime.Now.AddDays(7)
			};

		private EditInvoiceViewModel PrepareEditInvoiceViewModel(Invoice invoice, string userId)
			=> new EditInvoiceViewModel
			{
				Invoice = invoice,
				Heading = (invoice.Id == 0) ? "Dodawanie nowej faktury" : "Faktura",
				Clients = _clientRepository.GetClients(userId),
				MethodOfPayments = _invoiceRepository.GetMethodOfPayments()
			};

		private InvoicePosition GetNewInvoicePosition(int invoiceId, int invoicePositionId)
			=> new InvoicePosition
			{
				InvoiceId = invoiceId,
				Id = invoicePositionId,
			};

		private EditInvoicePositionViewModel PrepareEditInvoicePositionViewModel(InvoicePosition invoicePosition)
			=> new EditInvoicePositionViewModel
			{
				InvoicePosition = invoicePosition,
				Heading = (invoicePosition.Id == 0) ? "Dodawanie nowej pozycji" : "Pozycja",
				Products = _productRepository.GetProducts(),
			};

		#endregion Private methods

		#region Sesja

		// INFO - sesja
		// ta metoda będzie zawsze aktualizować sesję o wybranym kluczu o wartość podaną w argumencie
		// argument - tutaj jest to wartość o jaką będzie aktualizowana sesja
		// odwołanie do konkretnej sesji, w nawiasach kwadratowych trzeba podać klucz
		private void UpdateSession(int i) => Session["nr"] = i;

		// ta metoda pobiera sesję
		private int GetSession() => Session["nr"] != null ? (int)Session["nr"] : 0;

		#endregion Sesja

		#region Cookies - ciasteczka

		// INFO - Cookies - ciasteczka
		// dodawanie nowego cookie albo aktualizacja
		private void UpdateCookie(int i)
		{
			// pierwszy argument to nazwa ciastka, drugi to wartość jaka ma być przypisana, rzutować na typ string
			HttpCookie cookie = new HttpCookie("nr", i.ToString())
			{
				// data wygaśnięcia ciastka, gdy nie ustawione to ciastko istnieje tylko podczas uruchomienia w przegladarce, po zamknięciu ciastko jest kasowane
				Expires = DateTime.Now.AddDays(365)

				// ustawienie ujemnej daty powoduje usunięcie ciasteczka
				//Expires = DateTime.Now.AddDays(-1)
			};

			// w odpowiedzi ustawienie ciastka, response zawiera informacje jakie ciasteczka przeglądarka powinna zapisać
			Response.SetCookie(cookie);
		}

		// pobieranie ciastka; ciastka są wysyłane z każdym żądaniem wysyłanym do serwera
		// sprawdzenie czy cookie istnieje, request używa się do odczytania ciastka
		private int GetCookie() => Request.Cookies["nr"] != null ? int.Parse(Request.Cookies["nr"].Value) : 0;

		#endregion Cookies - ciasteczka

		#region Cache

		// INFO - Cache
		private void UpdateCache(int i) => HttpRuntime.Cache["nr"] = i;

		// inny sposób ustawienia wartości cache, argumenty to:
		// nazwa, wartość, zależność do pliku/tabelki, bezwzględny czas wygasania, czas przedłużający czas wygasania po użyciu cache, prirorytet - kolejność usuwania, delegat wywoływany podczas usuwania cache z pamięci
		//private void UpdateCache(int i) => HttpRuntime.Cache.Add("nr", i, null, DateTime.Now.AddSeconds(5), TimeSpan.Zero, CacheItemPriority.Default, null);

		private int GetCache() => HttpRuntime.Cache["nr"] != null ? (int)HttpRuntime.Cache["nr"] : 0;

		#endregion Cache

		#region Test Actions

		public ActionResult Test()
		{
			ViewBag.MyNumber = 666;
			return View("About");
		}

		public ActionResult Test1() => PartialView("About");

		public ActionResult Test2() => RedirectToAction("About", "Home");

		public ActionResult Test3()
		{
			Invoice invoice = new Invoice { Id = 3123, Title = "Test" };
			return Json(invoice, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Test4() => File("../Web.config", "text");

		public ActionResult Test5() => Content("<script>alert('ALERT!')</script>");

		[AllowAnonymous]
		// INFO - pobranie pliku z serwera
		// Home/GetFile?fileName=TestFile.zip
		public ActionResult GetFile(string fileName)
		{
			var physicalFilePath = Server.MapPath("~/Files/" + fileName);
			return File(physicalFilePath, "application/zip");
		}

		// INFO - własny Action Result
		public ActionResult GetCsv() => new MyCsvResult("Jan Kowalski" + Environment.NewLine + "Krzysztof Nowak");

		// INFO - ChildActionOnly
		// oznacza że nie można dostać się do tej akcji poprzez wpisanie adresu URL
		// czyli jest dostępne tylko jako podzapytanie gdy przetwarzany jest główny widok
		// umożliwia przekazanie modelu do widoku częściowego, w którym można się odwołać do @Model, bez wcześniejszej deklaracji modelu
		[ChildActionOnly]
		[AllowAnonymous]
		public ActionResult GetTimePartial()
		{
			var time = DateTime.Now.ToLongTimeString();
			return PartialView("PartialTime", time);
		}

		[ChildActionOnly]
		[AllowAnonymous]
		public string GetTime() => DateTime.Now.ToLongTimeString();

		#endregion Test Actions
	}
}
using System;
using System.Collections.Generic;
using SendReport.ServiceApp.Models.Domain;

namespace SendReport.ServiceApp.Repositories
{
	public class ErrorRepository
	{
		// pobieranie z bazy danych
		public List<Error> GetLastErrors(int intervalInMinutes)
			=> new List<Error>
			{
				new Error { Message = "Błąd testowy 1", Date= DateTime.Now },
				new Error { Message = "Błąd testowy 2", Date= DateTime.Now },
			};
	}
}
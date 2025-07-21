using System;
using System.Collections.Generic;
using EmailSender.NetFramework.Lib;
using SendReport.Core.NetFramework.Lib;
using SendReport.Core.NetFramework.Lib.Models.Domains;

namespace SendReport.NetFramework.ConsoleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var emailReceiver = "jakiesMail@testowy.pl";

			var htmlEmail = new GenerateHtmlEmail();

			var email = new Email(new EmailParams
			{
				HostSmtp = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				SenderName = "Imie Nazwisko",
				SenderEmail = "",
				SenderEmailPassword = "",
			});

			var report = new Report
			{
				ReportId = 1,
				Title = "R/1/2020",
				Date = new DateTime(2020, 1, 1, 12, 0, 0),
				Positions = new List<ReportPosition>
				{
					new ReportPosition
					{
						ReportPositionId = 1,
						Title = "Position 1",
						Description = "Descriptopn 1",
						Value = 11.11M,
					},
					new ReportPosition
					{
						ReportPositionId = 2,
						Title = "Position 2",
						Description = "Descriptopn 2",
						Value = 22.22M,
					},
					new ReportPosition
					{
						ReportPositionId = 3,
						Title = "Position 3",
						Description = "Descriptopn 3",
						Value = 33.33M,
					}
				 }
			};

			var errors = new List<Error>
			{
				new Error { Message = "Błąd testowy 1", Date= DateTime.Now },
				new Error { Message = "Błąd testowy 2", Date= DateTime.Now },
			};

			Console.WriteLine("Wysyłanie email (raport dobowy)");
			email.Send("Raport dobowy", htmlEmail.GenerateReport(report), emailReceiver).Wait();
			Console.WriteLine("Wysyłano email (raport dobowy)");

			Console.WriteLine("Wysyłanie email (błędy w aplikacji)");
			email.Send("Błędy w aplikacji", htmlEmail.GenerateErrors(errors, 10), emailReceiver).Wait();
			Console.WriteLine("Wysyłano email (błędy w aplikacji)");
		}
	}
}
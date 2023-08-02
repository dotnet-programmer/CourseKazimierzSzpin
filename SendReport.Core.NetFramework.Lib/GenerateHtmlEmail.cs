using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendReport.Core.NetFramework.Lib.Models.Domains;

namespace SendReport.Core.NetFramework.Lib
{
	public class GenerateHtmlEmail
	{
		public string GenerateErrors(List<Error> errors, int interval)
		{
			if (errors == null)
			{
				throw new ArgumentNullException(nameof(errors));
			}

			if (!errors.Any())
			{
				return string.Empty;
			}

			StringBuilder html = new StringBuilder();
			html.Append($"Błędy z ostatnich {interval} minut. <br /><br />");
			html.Append(@"
				<table border=1 cellpadding=5 cellspacing=1>
					<tr>
						<td align=center bgcolor=lightgrey>Wiadomość</td>
						<td align=center bgcolor=lightgrey>Data</td>
					</tr>
				");
			foreach (var error in errors)
			{
				html.Append($@"
					<tr>
						<td align=center>{error.Message}</td>
						<td align=center>{error.Date:dd.MM.yyyy HH:mm}</td>
					</tr>
				");
			}

			html.Append(@"</table><br /><br />");

			return PrepareHtmlMessage(html.ToString());
		}

		public string GenerateReport(Report report)
		{
			if (report == null)
			{
				throw new ArgumentNullException(nameof(report));
			}

			StringBuilder html = new StringBuilder();
			html.Append($"Raport {report.Title} z dnia {report.Date:dd.MM.yyyy}. <br /><br />");

			if (report.Positions != null && report.Positions.Any())
			{
				html.Append(@"
				<table border=1 cellpadding=5 cellspacing=1>
					<tr>
						<td align=center bgcolor=lightgrey>Tytuł</td>
						<td align=center bgcolor=lightgrey>Opis</td>
						<td align=center bgcolor=lightgrey>Wartość</td>
					</tr>
				");

				foreach (var position in report.Positions)
				{
					html.Append($@"
					<tr>
						<td align=center>{position.Title}</td>
						<td align=center>{position.Description}</td>
						<td align=center>{position.Value:0.00} zł</td>
					</tr>
				");
				}

				html.Append(@"</table>");
			}
			else
			{
				html.Append("-- Brak danych do wyświetlenia --");
			}
			html.Append(@"<br /><br />");

			return PrepareHtmlMessage(html.ToString());
		}

		private string PrepareHtmlMessage(string body)
			=> $@"<html>
					<head>
					</head>
					<body>
						<div style='font-size: 16px padding: 10px; font-family: Arial; line-height: 1.4;'>
						{body}
						<i>Automatyczna wiadomość wysłana z aplikacji ReportService.</i>
						</div>
					</body>
				</html>";
	}
}
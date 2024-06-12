using System;
using System.Collections.Generic;
using SendReport.Core.NetFramework.Lib.Models.Domains;

namespace SendReport.Core.NetFramework.Lib.Repositories
{
	public class ReportRepository
	{
		// pobieranie z bazy danych ostatniego raportu
		public Report GetLastNotSentReport()
			=> new Report
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

		public void ReportSent(Report report)
			=> report.IsSend = true; //zapis w bazie danych
	}
}
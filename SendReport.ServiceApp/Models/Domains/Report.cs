using System;
using System.Collections.Generic;

namespace SendReport.ServiceApp.Models.Domain
{
	public class Report
	{
		public int ReportId { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public bool IsSend { get; set; }
		public List<ReportPosition> Positions { get; set; }
	}
}
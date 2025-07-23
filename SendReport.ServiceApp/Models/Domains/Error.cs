using System;

namespace SendReport.ServiceApp.Models.Domain
{
	public class Error
	{
		public int ErrorId { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
	}
}
using System;

namespace SendReport.Core.NetFramework.Lib.Models.Domains
{
	public class Error
	{
		public int ErrorId { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
	}
}
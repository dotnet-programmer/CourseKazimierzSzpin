namespace SendReport.ServiceApp.Models.Domain
{
	public class ReportPosition
	{
		public int ReportPositionId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public int ReportId { get; set; }
	}
}
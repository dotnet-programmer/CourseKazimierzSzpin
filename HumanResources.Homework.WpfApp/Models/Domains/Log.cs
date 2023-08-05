namespace HumanResources.Homework.WpfApp.Models.Domains;

public class Log
{
	public int LogId { get; set; }
	public DateTime Logged { get; set; }
	public string User { get; set; }
	public string Exception { get; set; }
}
namespace SendEmail.WebApp.Core.ViewModels;

public class IndexViewModel
{
	public string Heading { get; set; } = default!;
	public int NumberOfEmails { get; set; }
	public int NumberOfContacts { get; set; }
}
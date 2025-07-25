using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace SendEmail.WebApp.Core.Models.Domains;

public class ApplicationUser : IdentityUser
{
	public ICollection<Contact> Contacts { get; set; } = new Collection<Contact>();
	public ICollection<EmailSent> EmailsSent { get; set; } = new Collection<EmailSent>();
	public ICollection<Settings> Settings { get; set; } = new Collection<Settings>();
}
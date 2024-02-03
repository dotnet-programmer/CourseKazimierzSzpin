using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace MyTasks.WebApp.Core.Models.Domains;

public class ApplicationUser : IdentityUser
{
	public ICollection<Task> Tasks { get; set; } = new Collection<Task>();
	public ICollection<Category> Categories { get; set; } = new Collection<Category>();
}
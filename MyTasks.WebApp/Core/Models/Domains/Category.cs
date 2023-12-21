using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyTasks.WebApp.Core.Models.Domains;

public class Category
{
    public int CategoryId { get; set; }

	[Required]
	public string Name { get; set; }

	public ICollection<Task> Tasks { get; set; } = new Collection<Task>();
}

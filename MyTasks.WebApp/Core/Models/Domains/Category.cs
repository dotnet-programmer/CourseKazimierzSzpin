using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyTasks.WebApp.Core.Models.Domains;

public class Category
{
	public int CategoryId { get; set; }

	[Required(ErrorMessage = "Pole Nazwa jest wymagane")]
	[Display(Name = "Nazwa")]
	[MaxLength(250)]
	public string Name { get; set; }

    public bool? IsDefault { get; set; }

    public string? UserId { get; set; }
	public ApplicationUser? User { get; set; }

	public ICollection<Task> Tasks { get; set; } = new Collection<Task>();
}
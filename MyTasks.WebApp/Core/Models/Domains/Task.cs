using System.ComponentModel.DataAnnotations;

namespace MyTasks.WebApp.Core.Models.Domains;

public class Task
{
	public int TaskId { get; set; }

	[Required(ErrorMessage = "Pole Tytuł jest wymagane")]
	[Display(Name = "Tytuł")]
	[MaxLength(50)]
	public string Title { get; set; }

	[Required(ErrorMessage = "Pole Opis jest wymagane")]
	[Display(Name = "Opis")]
	[MaxLength(250)]
	public string Description { get; set; }

	[Display(Name = "Termin")]
	public DateTime? Term { get; set; }

	[Display(Name = "Zrealizowane")]
	public bool IsExecuted { get; set; }

	[Required(ErrorMessage = "Pole Kategoria jest wymagane")]
	[Display(Name = "Kategoria")]
	public int CategoryId { get; set; }

	public string UserId { get; set; }
	
	public Category Category { get; set; }
	public ApplicationUser User { get; set; }
}
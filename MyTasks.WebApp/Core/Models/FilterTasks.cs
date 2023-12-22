using System.ComponentModel.DataAnnotations;

namespace MyTasks.WebApp.Core.Models;

public class FilterTasks
{
	[Display(Name = "Tytuł")]
	public string Title { get; set; }

	[Display(Name = "Kategoria")]
	public int CategoryId { get; set; }

	[Display(Name = "Tylko zrealizowane")]
	public bool IsExecuted { get; set; }
}
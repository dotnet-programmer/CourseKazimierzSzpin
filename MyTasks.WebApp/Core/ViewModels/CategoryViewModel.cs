using MyTasks.WebApp.Core.Models.Domains;

namespace MyTasks.WebApp.Core.ViewModels;

public class CategoryViewModel
{
	public string Heading { get; set; }
	public Category Category { get; set; }
}
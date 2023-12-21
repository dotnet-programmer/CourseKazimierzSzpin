using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core.ViewModels;

public class TasksViewModel
{
	public IEnumerable<Task> Tasks { get; set; }
	public IEnumerable<Category> Categories { get; set; }
	public FilterTasks FilterTasks { get; set; }
}
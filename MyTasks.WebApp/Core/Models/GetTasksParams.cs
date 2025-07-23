namespace MyTasks.WebApp.Core.Models;

public class GetTasksParams
{
	public string UserId { get; set; }
	public FilterTasks FilterTasks { get; set; } = new();
}
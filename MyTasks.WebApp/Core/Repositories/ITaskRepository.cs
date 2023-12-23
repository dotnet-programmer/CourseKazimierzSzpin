using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core.Repositories;

public interface ITaskRepository
{
	IEnumerable<Task> GetTasks(string? userId, bool isExecuted = false, int categoryId = 0, string title = null);

	IEnumerable<Category> GetCategories();

	Task GetTask(int taskId, string userId);

	void AddTask(Task task);

	void UpdateTask(Task task);

	void DeleteTask(int taskId, string userId);

	void FinishTask(int taskId, string userId);
}
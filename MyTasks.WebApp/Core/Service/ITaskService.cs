using MyTasks.WebApp.Core.Models;
using MyTasks.WebApp.Core.Models.Domains;
using Task = MyTasks.WebApp.Core.Models.Domains.Task;

namespace MyTasks.WebApp.Core.Service;

public interface ITaskService
{
	IEnumerable<Task> GetTasks(GetTaskParams getTaskParams);

	IEnumerable<Category> GetCategories();

	Task GetTask(int taskId, string userId);

	void AddTask(Task task);

	void UpdateTask(Task task);

	void DeleteTask(int taskId, string userId);

	void FinishTask(int taskId, string userId);
}